using MediatR;
using SurveyMaker.Application.Models.Dtos;
using SurveyMaker.Application.Services;
using SurveyMaker.Domain.Entities;
using SurveyMaker.Domain.Repositories;

namespace SurveyMaker.Application.Features.CreateOption
{
    public class CreateOptionCommandHandler : IRequestHandler<CreateOptionCommand, OptionDto>
    {
        private readonly IUserContext _userContext;
        private readonly ISurveyRepository _surveyRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IOptionRepository _optionRepository;

        public CreateOptionCommandHandler(IUserContext userContext, ISurveyRepository surveyRepository, IQuestionRepository questionRepository, IOptionRepository optionRepository)
        {
            _userContext = userContext;
            _surveyRepository = surveyRepository;
            _questionRepository = questionRepository;
            _optionRepository = optionRepository;
        }


        public async Task<OptionDto> Handle(CreateOptionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetByIdAsync(request.QuestionId, cancellationToken);

            if (question == null)
            {
                throw new NullReferenceException($"Question with id {request.QuestionId} not found.");
            }
            var survey = await _surveyRepository.GetByIdAsync(question.SurveyId);

            if (survey == null)
            {
                throw new NullReferenceException($"Survey with id {question.SurveyId} not found.");
            }

            if (Guid.Parse(survey.CreatedBy) != _userContext.UserId)
            {
                throw new UnauthorizedAccessException("You are not allowed to create options for this question.");
            }

            var opcion = Option.Create(request.Text, question.Id);

            await _optionRepository.SaveAsync(opcion, cancellationToken);

            return OptionDto.Create(opcion);

        }
    }
}
