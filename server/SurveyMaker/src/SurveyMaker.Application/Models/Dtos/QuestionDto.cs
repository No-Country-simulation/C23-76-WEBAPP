namespace SurveyMaker.Application.Models.Dtos
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int? MaxSelections { get; set; }
        public virtual ICollection<OptionDto> Options { get; set; }
    }
}
