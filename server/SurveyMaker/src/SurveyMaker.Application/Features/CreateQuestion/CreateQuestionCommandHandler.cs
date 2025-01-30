using MediatR;
using SurveyMaker.Application.Models.Dtos;
using SurveyMaker.Application.Services;
using SurveyMaker.Domain.Entities;
using SurveyMaker.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMaker.Application.Features.CreateQuestion
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, QuestionDto>
    {
        private readonly IUserContext _userContext;
        private readonly ISurveyRepository _surveyRepository;
        private readonly IQuestionRepository _questionRepository;

        public CreateQuestionCommandHandler(IUserContext userContext, ISurveyRepository surveyRepository, IQuestionRepository questionRepository)
        {
            _userContext = userContext;
            _surveyRepository = surveyRepository;
            _questionRepository = questionRepository;
        }

        public async Task<QuestionDto> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var survey = await _surveyRepository.GetByIdAsync(request.SurveyId);

            if (survey == null)
            {
                throw new NullReferenceException("Survey not found");
            }

            if(survey.CreatedBy != _userContext.UserId.ToString())
            {
                throw new UnauthorizedAccessException("You are not allowed to create questions for this survey");
            }

            var question = Question.Create(
                title: request.Title,
                maxSelections: request.MaxSelections,
                type: request.Type,
                options: request.Options.Select(x => Option.Create(
                    text: x.Text))
                .ToList());

            question.SurveyId = survey.Id;

            await _questionRepository.SaveAsync(question, cancellationToken);

            return QuestionDto.Create(question);

        }
    }
}
