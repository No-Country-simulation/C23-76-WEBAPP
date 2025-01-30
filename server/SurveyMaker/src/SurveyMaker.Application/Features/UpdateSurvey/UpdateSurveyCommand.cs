using MediatR;
using SurveyMaker.Application.Models.Dtos;

namespace SurveyMaker.Application.Features.UpdateSurvey
{
    public class UpdateSurveyCommand : IRequest<SurveyDto>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public int? VotesAmountRequiredToFinish { get; set; }
    }
}
