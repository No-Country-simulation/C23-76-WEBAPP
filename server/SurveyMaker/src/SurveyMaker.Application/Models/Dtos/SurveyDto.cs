using SurveyMaker.Domain.Entities;
using SurveyMaker.Domain.Enums;

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
                VotesAmountRequiredToFinish = survey.VotesAmountRequiredToFinish
            };
        }
    }
}
