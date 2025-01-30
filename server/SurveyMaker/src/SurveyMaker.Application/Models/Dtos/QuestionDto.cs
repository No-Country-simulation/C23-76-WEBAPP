using SurveyMaker.Domain.Entities;

namespace SurveyMaker.Application.Models.Dtos
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int? MaxSelections { get; set; }
        public virtual ICollection<OptionDto> Options { get; set; }


        public static QuestionDto Create(Question question)
        {
            return new QuestionDto
            {
                Id = question.Id,
                Title = question.Title,
                Type = question.Type.ToString(),
                MaxSelections = question.MaxSelections,
                Options = question.Options
                    .Select(option => new OptionDto
                    {
                        Id = option.Id,
                        Text = option.Text
                    })
                    .ToList()
            };
        }

    }
}
