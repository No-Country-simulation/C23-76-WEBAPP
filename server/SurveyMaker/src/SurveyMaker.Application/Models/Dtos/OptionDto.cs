using SurveyMaker.Domain.Entities;

namespace SurveyMaker.Application.Models.Dtos
{
    public class OptionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public static OptionDto Create(Option option)
        {
            return new OptionDto
            {
                Id = option.Id,
                Text = option.Text
            };
        }
    }
}
