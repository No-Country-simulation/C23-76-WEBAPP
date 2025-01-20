using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SurveyMaker.Infrastructure.EF
{
    public class SurveyMakerContextFactory : IDesignTimeDbContextFactory<SurveyMakerDbContext>
    {
        public SurveyMakerDbContext CreateDbContext(string[] args)
        {
            //TODO: This should be update to use a real DB
            var builder = new DbContextOptionsBuilder<SurveyMakerDbContext>();

            builder.UseNpgsql("SurveyMakerDb");

            return new SurveyMakerDbContext(builder.Options);
        }
    }
}
