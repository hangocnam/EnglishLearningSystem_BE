using EnglishLearningSystem.API.Persistence;
using EnglishLearningSystem.Application.Interfaces;

namespace EnglishLearningSystem.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPI(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurentUser>();
            return services;
        }
    }
}
