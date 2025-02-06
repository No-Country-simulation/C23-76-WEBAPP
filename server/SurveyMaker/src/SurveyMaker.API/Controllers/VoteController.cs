using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyMaker.API.Models.Requests;
using SurveyMaker.Application.Features.CreateVote;
using SurveyMaker.Application.Models.Dtos;

namespace SurveyMaker.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType<VoteDto>(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateVoteAsync([FromBody] CreateVoteRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateVoteCommand
            {
                SurveyId = request.SurveyId,
                VotedAt = request.VotedAt,
                Answers = request.Answers.Select(a => new CreateVoteAnswerDto
                {
                    QuestionId = a.QuestionId,
                    selectedOptionsIds = a.selectedOptionsIds
                }).ToList()
            };

            var result = await _mediator.Send(command, cancellationToken);

            return Created(Request.Path, result);
        }
    }
}
