using MediatR;
using SurveyMaker.Application.Models.Dtos;

namespace SurveyMaker.Application.Features.CreateSurvey
{
    public class CreateSurveyCommand : IRequest<SurveyDto>
    {
        public string Title { get; set; }
        public DateTime? StartsAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool AllowAnonymousVotes { get; set; }
        public int? VotesAmountRequiredToFinish { get; set; }
    }
}
