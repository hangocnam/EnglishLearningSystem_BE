using EnglishLearningSystem.API;
using EnglishLearningSystem.API.Errors;
using EnglishLearningSystem.Application;
using EnglishLearningSystem.Infrastructure;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddWebAPI();

// Error handling: every unhandled exception → ProblemDetails with a Domain error code
builder.Services.AddProblemDetails(options =>
    options.CustomizeProblemDetails = context =>
    {
        var problem = context.ProblemDetails;

        problem.Type = null;

        if (!problem.Extensions.ContainsKey(ErrorHttpMapping.ErrorCodeKey))
        {
            var status = problem.Status ?? context.HttpContext.Response.StatusCode;
            problem.ApplyErrorCode(context.HttpContext, ErrorHttpMapping.FallbackErrorCodeFor(status));
        }
    });
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập JWT access token (không cần tiền tố \"Bearer \")"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRequestLocalization();
app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
