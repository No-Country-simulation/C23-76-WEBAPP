namespace SurveyMaker.API.Models.Requests
{
    public class UpdateQuestionRequest
    {
        public int SurveyId { get; set; }
        public string? Title { get; set; }
        public int? MaxSelections { get; set; }
    }
}
