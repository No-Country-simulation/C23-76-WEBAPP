using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyMaker.API.Models.Requests;
using SurveyMaker.Application.Features.CreateSurvey;
using SurveyMaker.Application.Features.GetSurveyList;
using SurveyMaker.Application.Features.UpdateSurvey;
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


        
        [HttpPut("{surveyId}")]
        [Authorize]
        [ProducesResponseType<SurveyDto>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] int surveyId, [FromBody] UpdateSurveyRequest request)
        {
            var result = await _mediator.Send(new UpdateSurveyCommand
            {
                Id = surveyId,
                Title = request.Title,
                ExpiresAt = request.ExpiresAt,
                VotesAmountRequiredToFinish = request.VotesAmountRequiredToFinish
            });

            return Ok(result);
        }


        [HttpPost("list/private")]
        [Authorize]
        [ProducesResponseType<List<SurveyDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPrivateList(SurveyListRequest request)
        {
            var result = await _mediator.Send(new GetSurveyListQuery
            {
                isPersonal = false,
                IsAuthenticated = true,
                withQuestions = request.WithQuestions,
                withOptions = request.WithOptions
            });

            return Ok(result);
        }

        [HttpPost("list/public")]
        [ProducesResponseType<List<SurveyDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPublicList(SurveyListRequest request)
        {
            var result = await _mediator.Send(new GetSurveyListQuery
            {
                IsAuthenticated = false,
                withQuestions = request.WithQuestions,
                withOptions = request.WithOptions
            });

            return Ok(result);
        }


        [HttpPost("list/user")]
        [Authorize]
        [ProducesResponseType<List<SurveyDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListByUser(SurveyListRequest request)
        {
            var result = await _mediator.Send(new GetSurveyListQuery
            {
                isPersonal = true,
                IsAuthenticated = true,
                withQuestions = request.WithQuestions,
                withOptions = request.WithOptions
            });

            return Ok(result);
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
