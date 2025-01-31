using MediatR;
using SurveyMaker.Application.Models.Dtos;

namespace SurveyMaker.Application.Features.GetSurveyList
{
    public class GetSurveyListQuery : IRequest<List<SurveyDto>>
    {
        public bool IsAuthenticated { get; set; }
    }
}
