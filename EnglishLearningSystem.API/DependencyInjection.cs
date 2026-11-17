using System.IdentityModel.Tokens.Jwt;
using System.Text;
using EnglishLearningSystem.API.Errors;
using EnglishLearningSystem.API.Persistence;
using EnglishLearningSystem.Application.Interfaces;
using EnglishLearningSystem.Domain.Common.Results;
using EnglishLearningSystem.Domain.Errors;
using EnglishLearningSystem.Infrastructure.Securities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EnglishLearningSystem.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPI(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurentUser>();

            services.AddJwtAuthentication();
            services.AddInvalidModelStateResponse();

            return services;
        }

        private static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

            // Read settings from JwtSettings bound in Infrastructure (same source as TokenService)
            services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
                .Configure<IOptions<JwtSettings>>((options, jwtOptions) =>
                {
                    var jwt = jwtOptions.Value;

                    // Keep the original claim names ("sub", "name", "role"); do not map them to ClaimTypes.*
                    options.MapInboundClaims = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwt.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwt.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey)),
                        ValidAlgorithms = [SecurityAlgorithms.HmacSha256],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(30),
                        NameClaimType = JwtRegisteredClaimNames.Name,
                        RoleClaimType = AppClaimTypes.Role
                    };

                    options.Events = new JwtBearerEvents
                    {
                        // Return 401 as ProblemDetails with a Domain error code instead of an empty body
                        OnChallenge = async context =>
                        {
                            context.HandleResponse();

                            var error = context.AuthenticateFailure is SecurityTokenExpiredException
                                ? AuthErrors.TokenExpired
                                : AuthErrors.Unauthenticated;

                            var problem = error.ToProblemDetails(context.HttpContext);
                            context.Response.StatusCode = problem.Status!.Value;
                            context.Response.Headers.WWWAuthenticate = JwtBearerDefaults.AuthenticationScheme;

                            var problemDetailsService = context.HttpContext.RequestServices.GetRequiredService<IProblemDetailsService>();
                            await problemDetailsService.WriteAsync(new ProblemDetailsContext
                            {
                                HttpContext = context.HttpContext,
                                ProblemDetails = problem
                            });
                        }
                    };
                });

            return services;
        }

        private static IServiceCollection AddInvalidModelStateResponse(this IServiceCollection services)
        {
            // Model binding errors (malformed JSON, wrong types) use the same General.ValidationFailed format
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var fieldErrors = context.ModelState
                        .Where(e => e.Value is { Errors.Count: > 0 })
                        .Select(e => new FieldError(e.Key, ValidationErrors.InvalidFormat))
                        .ToList();

                    var problem = GeneralErrors.ValidationFailed(fieldErrors).ToProblemDetails(context.HttpContext);
                    return new ObjectResult(problem)
                    {
                        StatusCode = problem.Status,
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });

            return services;
        }
    }
}
