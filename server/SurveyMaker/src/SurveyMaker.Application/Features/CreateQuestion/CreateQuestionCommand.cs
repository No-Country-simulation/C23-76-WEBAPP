using MediatR;
using SurveyMaker.Application.Features.CreateSurvey;
using SurveyMaker.Application.Models.Dtos;
using SurveyMaker.Domain.Enums;

namespace SurveyMaker.Application.Features.CreateQuestion
{
    public class CreateQuestionCommand : IRequest<QuestionDto>
    {
        public int SurveyId { get; set; }
        public string Title { get; set; }
        public int? MaxSelections { get; set; }
        public QuestionType Type { get; set; }
        public ICollection<CreateSurveyOptionDto> Options { get; set; }
    }
}
