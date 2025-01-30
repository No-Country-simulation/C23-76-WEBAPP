namespace SurveyMaker.API.Models.Requests
{
    public class UpdateSurveyRequest
    {
        public string? Title { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public int? VotesAmountRequiredToFinish { get; set; }

    }
}
