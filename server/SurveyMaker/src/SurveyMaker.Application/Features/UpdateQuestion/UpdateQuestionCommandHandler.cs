using MediatR;
using SurveyMaker.Application.Models.Dtos;
using SurveyMaker.Application.Services;
using SurveyMaker.Domain.Entities;
using SurveyMaker.Domain.Repositories;

namespace SurveyMaker.Application.Features.UpdateQuestion
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, QuestionDto>
    {
        private readonly IUserContext _userContext;
        private readonly ISurveyRepository _surveyRepository;
        private readonly IQuestionRepository _questionRepository;

        public UpdateQuestionCommandHandler(IUserContext userContext, ISurveyRepository surveyRepository, IQuestionRepository questionRepository)
        {
            _userContext = userContext;
            _surveyRepository = surveyRepository;
            _questionRepository = questionRepository;
        }

        public async Task<QuestionDto> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var survey = await _surveyRepository.GetByIdAsync(request.SurveyId);
            if (survey == null)
            {
                throw new NullReferenceException("Survey not found");
            }

            if(Guid.Parse(survey.CreatedBy) != _userContext.UserId)
            {
                throw new UnauthorizedAccessException("You are not allowed to update questions for this survey");
            }

            var question = survey.Questions.FirstOrDefault(x => x.Id == request.Id);
            if (question == null)
            {
                throw new NullReferenceException("Question not found");
            }

            question = Question.Update(question, request.Title, request.MaxSelections);

            await _questionRepository.UpdateAsync(question, cancellationToken);

            return QuestionDto.Update(question);

        }
    }
}
