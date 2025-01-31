using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyMaker.API.Models.Requests;
using SurveyMaker.Application.Features.CreateSurvey;
using SurveyMaker.Application.Features.GetSurveyList;
using SurveyMaker.Application.Features.UpdateSurvey;
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


        
        [HttpPut("{surveyId}")]
        [Authorize]
        [ProducesResponseType<SurveyDto>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(string surveyId, [FromBody] UpdateSurveyRequest request)
        {
            var result = await _mediator.Send(new UpdateSurveyCommand
            {
                Id = int.Parse(surveyId),
                Title = request.Title,
                ExpiresAt = request.ExpiresAt,
                VotesAmountRequiredToFinish = request.VotesAmountRequiredToFinish
            });

            return Ok(result);
        }


        [HttpGet("list/private")]
        [Authorize]
        [ProducesResponseType<List<SurveyDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListByUser()
        {
            var result = await _mediator.Send(new GetSurveyListQuery
            {
                IsAuthenticated = true
            });

            return Ok(result);
        }

        [HttpGet("list/public")]
        [ProducesResponseType<List<SurveyDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetSurveyListQuery
            {
                IsAuthenticated = false
            });

            return Ok(result);
        }

    }
}
