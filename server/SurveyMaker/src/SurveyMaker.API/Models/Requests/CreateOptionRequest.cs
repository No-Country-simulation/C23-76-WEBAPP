namespace SurveyMaker.API.Models.Requests
{
    public class CreateOptionRequest
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
    }
}
