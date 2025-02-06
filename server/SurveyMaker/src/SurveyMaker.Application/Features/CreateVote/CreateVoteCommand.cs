using MediatR;
using SurveyMaker.Application.Models.Dtos;

namespace SurveyMaker.Application.Features.CreateVote
{
    public class CreateVoteCommand : IRequest<VoteDto>
    {
        public int SurveyId { get; set; }
        public DateTime VotedAt { get; set; }
        public ICollection<CreateVoteAnswerDto> Answers { get; set; }
    }

    public class CreateVoteAnswerDto
    {
        public int QuestionId { get; set; }
        public ICollection<int> selectedOptionsIds { get; set; }
    }
}
