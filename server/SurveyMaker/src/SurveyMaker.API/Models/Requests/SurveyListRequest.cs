namespace SurveyMaker.API.Models.Requests
{
    public class SurveyListRequest
    {
        public bool WithQuestions { get; set; }
        public bool WithOptions { get; set; }
    }
}
