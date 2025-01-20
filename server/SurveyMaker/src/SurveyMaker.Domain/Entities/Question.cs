using SurveyMaker.Domain.Enums;

namespace SurveyMaker.Domain.Entities
{
    public class Question
    {
        public Question()
        {
            Options = new List<Option>();    
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int SurveyId { get; set; }
        public virtual Survey? Survey { get; set; }
        public QuestionType Type { get; set; }
        public int? MaxSelections { get; set; }
        public virtual ICollection<Option> Options { get; set; }
    }
}
