using MediatR;
using SurveyMaker.Application.Models.Dtos;

namespace SurveyMaker.Application.Features.UpdateQuestion
{
    public class UpdateQuestionCommand : IRequest<QuestionDto>
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string? Title { get; set; }
        public int? MaxSelections { get; set; }
    }
}
