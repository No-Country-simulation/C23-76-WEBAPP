using MediatR;
using SurveyMaker.Application.Models.Dtos;
using SurveyMaker.Application.Services;
using SurveyMaker.Domain.Entities;
using SurveyMaker.Domain.Repositories;

namespace SurveyMaker.Application.Features.UpdateSurvey
{
    public class UpdateSurveyCommandHandler : IRequestHandler<UpdateSurveyCommand, SurveyDto>
    {
        private readonly IUserContext _userContext;
        private readonly ISurveyRepository _surveyRepository;

        public UpdateSurveyCommandHandler(IUserContext userContext, ISurveyRepository surveyRepository)
        {
            _userContext = userContext;
            _surveyRepository = surveyRepository;
        }

        public async Task<SurveyDto> Handle(UpdateSurveyCommand request, CancellationToken cancellationToken)
        {
            var survey = await _surveyRepository.GetByIdAsync(request.Id);

            if (survey == null)
            {
                throw new NullReferenceException("Survey not found.");
            }

            if (Guid.Parse(survey.CreatedBy) != _userContext.UserId)
            {
                throw new UnauthorizedAccessException("You are not allowed to update this survey.");
            }

            survey = Survey.Update(
                survey,
                request.Title,
                request.ExpiresAt,
                request.VotesAmountRequiredToFinish
            );

            await _surveyRepository.UpdateAsync(survey, cancellationToken);

            return SurveyDto.Create(survey);
        }
    }
}
