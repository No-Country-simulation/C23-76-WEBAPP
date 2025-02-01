using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyMaker.API.Models.Requests;
using SurveyMaker.Application.Features.CreateSurvey;
using SurveyMaker.Application.Features.GetSurveyLink;
using SurveyMaker.Application.Models.Dtos;

namespace SurveyMaker.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class SurveyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SurveyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType<SurveyDto>(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateSurveyRequest request)
        {
            var result = await _mediator.Send(new CreateSurveyCommand
            {
                AllowAnonymousVotes = request.AllowAnonymousVotes,
                ExpiresAt = request.ExpiresAt,
                StartsAt = request.StartsAt,
                Title = request.Title,
                VotesAmountRequiredToFinish = request.VotesAmountRequiredToFinish,
                Questions = request.Questions.Select(x => new CreateSurveyQuestionDto
                {
                    MaxSelections = x.MaxSelections,
                    Title = x.Title,
                    Type = x.Type,
                    Options = x.Options.Select(y => new CreateSurveyOptionDto
                    {
                        Text = y.Text,
                    })
                    .ToList()
                })
                .ToList()
            });

            return Created(Request.Path, result);
        }


        [HttpGet("link/{surveyId}")]
        [ProducesResponseType<SurveyLinkDto>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSurveyLink([FromRoute] int surveyId)
        {
            var userIsAuthenticated = User.Identity.IsAuthenticated;

            var result = await _mediator.Send(new GetSurveyLinkQuery
            {
                SurveyId = surveyId,
                UserIsAuthenticated = userIsAuthenticated
            });


            return Ok(result);
        }

    }

}
