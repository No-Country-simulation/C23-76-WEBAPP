using MediatR;
using SurveyMaker.Application.Models.Dtos;

namespace SurveyMaker.Application.Features.UpdateOption
{
    public class UpdateOptionCommand : IRequest<OptionDto>
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
