using SurveyMaker.Domain.Entities;
using SurveyMaker.Domain.Enums;
using System.Text.Json.Serialization;

namespace SurveyMaker.API.Models.Requests
{
    public class CreateQuestionRequest
    {
        public int SurveyId { get; set; }
        public string Title { get; set; }
        public int? MaxSelections { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public QuestionType Type { get; set; }
        public ICollection<CreateSurveyOptionRequest> Options { get; init; }
    }
}
