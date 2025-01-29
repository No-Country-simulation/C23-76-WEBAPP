using MediatR;
using SurveyMaker.Application.Models.Dtos;
using SurveyMaker.Domain.Repositories;

namespace SurveyMaker.Application.Features.GetSurveyLink
{
    public class GetSurveyLinkQueryHandler : IRequestHandler<GetSurveyLinkQuery, SurveyLinkDto>
    {
        private readonly ISurveyRepository _surveyRepository;

        public GetSurveyLinkQueryHandler(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public async Task<SurveyLinkDto> Handle(GetSurveyLinkQuery request, CancellationToken cancellationToken)
        {
            var survey = await _surveyRepository.GetSurveyLinkAsync(request.SurveyId, cancellationToken);

            if (survey is null)
            {
                throw new NullReferenceException("Survey not found.");
            }

            if (request.UserIsAuthenticated || 
                (!request.UserIsAuthenticated && survey.AllowAnonymousVotes))
            {
                return SurveyLinkDto.Create(survey);
            }

            throw new UnauthorizedAccessException("Survey is not public. Authentication required.");
        }
    }
}
