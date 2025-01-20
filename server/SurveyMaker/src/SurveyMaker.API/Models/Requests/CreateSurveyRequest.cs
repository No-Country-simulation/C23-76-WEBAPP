namespace SurveyMaker.API.Models.Requests
{
    public record CreateSurveyRequest(
        string Title,
        DateTime? StartsAt,
        DateTime? ExpiresAt,
        bool AllowAnonymousVotes,
        int? VotesAmountRequiredToFinish);
}
