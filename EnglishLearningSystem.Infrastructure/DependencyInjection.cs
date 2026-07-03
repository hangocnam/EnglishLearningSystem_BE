using EnglishLearningSystem.Application.Interfaces;
using EnglishLearningSystem.Application.Repositories;
using EnglishLearningSystem.Infrastructure.Persistence.EFCore;
using EnglishLearningSystem.Infrastructure.Persistence.EFCore.Repositories;
using EnglishLearningSystem.Infrastructure.Securities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearningSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services
                .AddConfigs(configuration)
                .AddServices()
                .AddRepositories();

            return services;
        }

        private static IServiceCollection AddConfigs(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            //========DB connection======
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        private static IServiceCollection AddServices(
            this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static IServiceCollection AddRepositories(
            this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
