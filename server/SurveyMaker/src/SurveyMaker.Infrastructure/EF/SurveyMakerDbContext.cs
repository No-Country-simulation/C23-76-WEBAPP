using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurveyMaker.Domain.Entities;

namespace SurveyMaker.Infrastructure.EF
{
    public class SurveyMakerDbContext : IdentityDbContext<SurveyMakerUser>
    {
        public SurveyMakerDbContext(DbContextOptions<SurveyMakerDbContext> options) : base(options)
        {
        }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Survey
            builder.Entity<Survey>()
                .HasKey(x => x.Id);

            builder.Entity<Survey>()
                .HasMany(x => x.Questions)
                .WithOne(x => x.Survey)
                .HasForeignKey(x => x.SurveyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Question
            builder.Entity<Question>()
                .HasKey(x => x.Id);

            builder.Entity<Question>()
                .HasMany(x => x.Options)
                .WithOne(x => x.Question)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Option
            builder.Entity<Option>()
                .HasKey(x => x.Id);
        }
    }
}
