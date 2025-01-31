using Microsoft.Extensions.DependencyInjection;
using SurveyMaker.Application.Services;
using System.Reflection;

namespace SurveyMaker.Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<IUserContext, UserContext>();

            services.AddScoped<ISurveyUrlBuilder, SurveyUrlBuilder>();

            services.AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
