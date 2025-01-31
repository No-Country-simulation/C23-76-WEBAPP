using MediatR;
using SurveyMaker.Application.Models.Dtos;
using SurveyMaker.Application.Services;
using SurveyMaker.Domain.Repositories;

namespace SurveyMaker.Application.Features.GetSurveyList
{
    public class GetSurveyListQueryHandler : IRequestHandler<GetSurveyListQuery, List<SurveyDto>>
    {
        private readonly IUserContext _userContext;
        private readonly ISurveyRepository _surveyRepository;

        public GetSurveyListQueryHandler(IUserContext userContext, ISurveyRepository surveyRepository)
        {
            _userContext = userContext;
            _surveyRepository = surveyRepository;
        }

        public async Task<List<SurveyDto>> Handle(GetSurveyListQuery request, CancellationToken cancellationToken)
        {
            var surveys = request.IsAuthenticated
                ? await _surveyRepository.GetAllByUserAsync(_userContext.UserId, request.withQuestions, request.withOptions)
                : await _surveyRepository.GetAllAsync(request.withQuestions, request.withOptions);

            return SurveyDto.CreateList(surveys);

        }
    }
}
