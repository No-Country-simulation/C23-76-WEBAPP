using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyMaker.Application.Features.ExceptionHandlingBehavior;
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

            // Registra el manejador de excepciones globales
            services.AddScoped<IExceptionHandler, ExceptionHandler>();

            // Registra el Pipeline Behavior
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));

            services.AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
