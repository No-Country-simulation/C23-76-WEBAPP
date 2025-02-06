using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyMaker.Domain.Repositories;
using SurveyMaker.Infrastructure.EF;
using SurveyMaker.Infrastructure.Repositories;

namespace SurveyMaker.Infrastructure.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDatabase(configuration)
                .AddRepositories();
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SurveyMakerDbContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("SurveyMakerDb"));
            });

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IOptionRepository, OptionRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();
            services.AddScoped<IVoteCounterRepository, VoteCounterRepository>();

            return services;
        }
    }
}
