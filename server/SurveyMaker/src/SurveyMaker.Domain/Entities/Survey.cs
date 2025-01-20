using SurveyMaker.Domain.Enums;

namespace SurveyMaker.Domain.Entities
{
    public class Survey
    {
        public Survey()
        {
            Questions = new List<Question>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public SurveyType Type { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public DateTime? StartsAt { get; set; }
        public bool AllowAnonymousVotes { get; set; }
        public int? VotesAmountRequiredToFinish { get; set; }
        public string? Url { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
