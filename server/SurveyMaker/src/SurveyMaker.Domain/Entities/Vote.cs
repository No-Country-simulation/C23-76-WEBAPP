namespace SurveyMaker.Domain.Entities
{
    public class Vote
    {
        public Vote()
        {
            Answers = new List<VoteAnswer>();    
        }

        public int Id { get; set; }
        public int SurveyId { get; set; }
        public virtual Survey? Survey { get; set; }
        public string VoterId { get; set; }
        public DateTime VotedAt { get; set; }
        public virtual ICollection<VoteAnswer> Answers { get; set; }
    }

    public class VoteAnswer
    {
        public VoteAnswer()
        {
            SelectedOptionsIds = new List<int>();
        }

        public int QuestionId { get; set; }
        public ICollection<int> SelectedOptionsIds { get; set; }
    }
}
