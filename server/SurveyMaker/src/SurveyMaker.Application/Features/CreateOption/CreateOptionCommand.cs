using MediatR;
using SurveyMaker.Application.Models.Dtos;

namespace SurveyMaker.Application.Features.CreateOption
{
    public class CreateOptionCommand : IRequest<OptionDto>
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
    }
}
