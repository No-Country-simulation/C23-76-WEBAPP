namespace SurveyMaker.API.Models.Requests
{
    public class CreateVoteRequest
    {
        public int SurveyId { get; set; }
        public DateTime VotedAt { get; set; }
        public ICollection<CreateVoteAnswerRequest> Answers { get; set; }
    }

    public class CreateVoteAnswerRequest
    {
        public int QuestionId { get; set; }
        public ICollection<int> selectedOptionsIds { get; set; }
    }
}
