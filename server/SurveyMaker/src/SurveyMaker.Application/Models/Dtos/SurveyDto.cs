using SurveyMaker.Domain.Entities;

namespace SurveyMaker.Application.Models.Dtos
{
    public class SurveyDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public DateTime? StartsAt { get; set; }
        public bool AllowAnonymousVotes { get; set; }
        public int? VotesAmountRequiredToFinish { get; set; }
        public ICollection<QuestionDto> Questions { get; set; }

        public static SurveyDto Create(Survey survey)
        {
            return new SurveyDto
            {
                Id = survey.Id,
                AllowAnonymousVotes = survey.AllowAnonymousVotes,
                ExpiresAt = survey.ExpiresAt,
                StartsAt = survey.StartsAt,
                Title = survey.Title,
                Type = survey.Type.ToString(),
                VotesAmountRequiredToFinish = survey.VotesAmountRequiredToFinish,
                Questions = survey.Questions.Select(x => new QuestionDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    MaxSelections = x.MaxSelections,
                    Type = x.Type.ToString(),
                    Options = x.Options.Select(y => new OptionDto
                    {
                        Id = y.Id,
                        Text = y.Text,
                    })
                    .ToList()
                })
                .ToList()
            };
        }
    }
}
