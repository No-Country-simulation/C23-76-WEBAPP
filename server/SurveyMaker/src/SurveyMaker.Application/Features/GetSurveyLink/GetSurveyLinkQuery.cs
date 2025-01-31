using MediatR;
using SurveyMaker.Application.Models.Dtos;

namespace SurveyMaker.Application.Features.GetSurveyLink
{
    public class GetSurveyLinkQuery : IRequest<SurveyLinkDto>
    {
        public int SurveyId { get; set; }
        public bool UserIsAuthenticated { get; set; }
    }
}
