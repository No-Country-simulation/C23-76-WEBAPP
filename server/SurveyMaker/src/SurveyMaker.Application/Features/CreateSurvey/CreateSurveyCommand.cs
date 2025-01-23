using MediatR;
using SurveyMaker.Application.Models.Dtos;
using SurveyMaker.Domain.Enums;

namespace SurveyMaker.Application.Features.CreateSurvey
{
    public class CreateSurveyCommand : IRequest<SurveyDto>
    {
        public string Title { get; set; }
        public DateTime? StartsAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool AllowAnonymousVotes { get; set; }
        public int? VotesAmountRequiredToFinish { get; set; }
        public ICollection<CreateSurveyQuestionDto> Questions { get; set; }
    }

    public class CreateSurveyQuestionDto
    {
        public string Title { get; set; }
        public int? MaxSelections { get; set; }
        public QuestionType Type { get; set; }
        public ICollection<CreateSurveyOptionDto> Options { get; set; }
    }

    public class CreateSurveyOptionDto
    {
        public string Text { get; set; }
    }
}
