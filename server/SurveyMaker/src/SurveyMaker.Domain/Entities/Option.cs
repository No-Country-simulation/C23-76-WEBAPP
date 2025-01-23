namespace SurveyMaker.Domain.Entities
{
    public class Option
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public virtual Question? Question { get; set; }

        public static Option Create(string text)
        {
            return new Option { Text = text };
        }
    }
}
