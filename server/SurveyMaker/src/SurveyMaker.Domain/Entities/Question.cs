using SurveyMaker.Domain.Enums;
using SurveyMaker.Domain.Exceptions;

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

        public static Question Create(
            string title,
            int? maxSelections,
            QuestionType type,
            ICollection<Option> options)
        {
            if (type == QuestionType.YesNo && (maxSelections != null && maxSelections != 2))
            {
                throw new InvalidQuestionParametersException(
                    Constants.Errors.InvalidQuestionYesNoConfiguaration);
            }

            if (type == QuestionType.MultipleChoice || type == QuestionType.SingleChoice)
            {
                if (options.Count < 2)
                {
                    throw new InvalidQuestionParametersException(
                        Constants.Errors.InvalidQuestionOptionsMinAmount);
                }
                if (maxSelections != null && maxSelections > options.Count)
                {
                    throw new InvalidQuestionParametersException(
                        Constants.Errors.InvalidQuestionMoreSelectionsThanOptions);
                }
            }

            return new Question
            {
                Title = title,
                MaxSelections = maxSelections,
                Type = type,
                Options = options
            };
        }

        public static Question Update(Question question, string title, int? maxSelections)
        {
            question.Title = title ?? question.Title;
            question.MaxSelections = maxSelections ?? question.MaxSelections;
            return question;
        }
    }
}
