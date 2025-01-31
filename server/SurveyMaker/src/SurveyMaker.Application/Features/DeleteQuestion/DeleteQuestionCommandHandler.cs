using MediatR;
using SurveyMaker.Application.Services;
using SurveyMaker.Domain.Repositories;

namespace SurveyMaker.Application.Features.DeleteQuestion
{
    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, int>
    {
        private readonly IUserContext _userContext;
        private readonly ISurveyRepository _surveyRepository;
        private readonly IQuestionRepository _questionRepository;

        public DeleteQuestionCommandHandler(IUserContext userContext, ISurveyRepository surveyRepository, IQuestionRepository questionRepository)
        {
            _userContext = userContext;
            _surveyRepository = surveyRepository;
            _questionRepository = questionRepository;
        }

        public async Task<int> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetByIdAsync(request.Id);

            if (question == null)
            {
                throw new NullReferenceException($"Question with id {request.Id} not found.");
            }

            var survey = await _surveyRepository.GetByIdAsync(question.SurveyId);

            if (survey == null)
            {
                throw new NullReferenceException($"Survey with id {question.SurveyId} not found.");
            }

            if (Guid.Parse(survey.CreatedBy) != _userContext.UserId)
            {
                throw new UnauthorizedAccessException("You are not allowed to delete this question.");
            }

            return await _questionRepository.DeleteAsync(question, cancellationToken);

        }
    }
}
