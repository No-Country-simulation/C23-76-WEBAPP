using SurveyMaker.Domain.Enums;
using System.Text.Json.Serialization;

namespace SurveyMaker.API.Models.Requests
{
    public record CreateSurveyRequest(
        string Title,
        DateTime? StartsAt,
        DateTime? ExpiresAt,
        bool AllowAnonymousVotes,
        int? VotesAmountRequiredToFinish,
        ICollection<CreateSurveyQuestionRequest> Questions);

    public record CreateSurveyQuestionRequest
    {
        public string Title { get; init; }
        public int? MaxSelections { get; init; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public QuestionType Type { get; init; }
        public ICollection<CreateSurveyOptionRequest> Options { get; init; }
    }

    public record CreateSurveyOptionRequest(
        string Text);
}
