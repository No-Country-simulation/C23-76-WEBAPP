﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyMaker.API.Models.Requests;
using SurveyMaker.Application.Features.CreateSurvey;
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
                VotesAmountRequiredToFinish = request.VotesAmountRequiredToFinish
            });

            return Created(Request.Path, result);
        }
    }
}
