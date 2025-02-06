using MediatR;
using SurveyMaker.Application.Models.Dtos;

namespace SurveyMaker.Application.Features.GetSurveyList
{
    public class GetSurveyListQuery : IRequest<List<SurveyDto>>
    {
        public bool isPersonal { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool withQuestions { get; set; }
        public bool withOptions { get; set; }
    }
}
