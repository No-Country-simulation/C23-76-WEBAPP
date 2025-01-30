using MediatR;
using SurveyMaker.Application.Models.Dtos;
using SurveyMaker.Application.Services;
using SurveyMaker.Domain.Repositories;

namespace SurveyMaker.Application.Features.UpdateOption
{
    partial class UpdateOptionCommandHandler : IRequestHandler<UpdateOptionCommand, OptionDto>
    {
        private readonly IUserContext _userContext;
        private readonly ISurveyRepository _surveyRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IOptionRepository _optionRepository;

        public UpdateOptionCommandHandler(IUserContext userContext, ISurveyRepository surveyRepository, IQuestionRepository questionRepository, IOptionRepository optionRepository)
        {
            _userContext = userContext;
            _surveyRepository = surveyRepository;
            _questionRepository = questionRepository;
            _optionRepository = optionRepository;
        }

        public async Task<OptionDto> Handle(UpdateOptionCommand request, CancellationToken cancellationToken)
        {
            var option = await _optionRepository.GetByIdAsync(request.Id);
            if (option == null) 
            {
                throw new NullReferenceException($"Option with id {request.Id} not found.");
            }

            var question = await _questionRepository.GetByIdAsync(option.QuestionId, cancellationToken);
            if (question == null)
            {
                throw new NullReferenceException($"Question with id {option.QuestionId} not found.");
            }

            var survey = await _surveyRepository.GetByIdAsync(question.SurveyId);
            if (survey == null)
            {
                throw new NullReferenceException($"Survey with id {question.SurveyId} not found.");
            }

            if (Guid.Parse(survey.CreatedBy) != _userContext.UserId)
            {
                throw new UnauthorizedAccessException("You are not allowed to update options for this question.");
            }

            option.Text = request.Text ?? option.Text;

            await _optionRepository.UpdateAsync(option, cancellationToken);

            return OptionDto.Create(option);

        }
    }
}
