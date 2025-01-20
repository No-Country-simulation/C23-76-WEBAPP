using MediatR;
using SurveyMaker.Application.Models.Dtos;
using SurveyMaker.Application.Services;
using SurveyMaker.Domain.Entities;
using SurveyMaker.Domain.Repositories;

namespace SurveyMaker.Application.Features.CreateSurvey
{
    public class CreateSurveyCommandHandler : IRequestHandler<CreateSurveyCommand, SurveyDto>
    {
        private readonly IUserContext _userContext;
        private readonly ISurveyRepository _surveyRepository;
        private readonly ISurveyUrlBuilder _surveyUrlBuilder;

        public CreateSurveyCommandHandler(
            IUserContext userContext,
            ISurveyRepository surveyRepository,
            ISurveyUrlBuilder surveyUrlBuilder)
        {
            _userContext = userContext;
            _surveyRepository = surveyRepository;
            _surveyUrlBuilder = surveyUrlBuilder;
        }

        public async Task<SurveyDto> Handle(CreateSurveyCommand request, CancellationToken cancellationToken)
        {
            var survey = Survey.Create(
                title: request.Title,
                expiresAt: request.ExpiresAt,
                startsAt: request.StartsAt,
                allowAnonymousVotes: request.AllowAnonymousVotes,
                votesAmountRequiredToFinish: request.VotesAmountRequiredToFinish,
                createdBy: _userContext.UserId.ToString(),
                surveyLink: _surveyUrlBuilder.Build()
                );

            await _surveyRepository.SaveAsync(survey, cancellationToken);

            //TODO: Send notification

            return SurveyDto.Create(survey);
        }
    }
}
