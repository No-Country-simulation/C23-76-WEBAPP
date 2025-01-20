using SurveyMaker.Domain.Entities;
using SurveyMaker.Infrastructure.EF;

namespace SurveyMaker.API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            return services
                .AddIdentity()
                .AddApiControllers()
                .AddSwagger();
        }

        private static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentityApiEndpoints<SurveyMakerUser>()
                .AddEntityFrameworkStores<SurveyMakerDbContext>();

            return services;
        }

        private static IServiceCollection AddApiControllers(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
