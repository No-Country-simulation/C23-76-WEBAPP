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

        public static Option Create(string text, int questionId)
        {
            return new Option { QuestionId = questionId, Text = text };
        }
        
        public static Option Update(Option option)
        {
            return new Option
            {
                Id = option.Id,
                Text = option.Text,
                QuestionId = option.QuestionId
            };
        }
    }
}
