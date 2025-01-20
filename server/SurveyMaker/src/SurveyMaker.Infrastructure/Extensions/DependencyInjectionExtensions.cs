using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyMaker.Infrastructure.EF;

namespace SurveyMaker.Infrastructure.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDatabase(configuration);
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SurveyMakerDbContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("SurveyMakerDb"));
            });

            return services;
        }
    }
}
