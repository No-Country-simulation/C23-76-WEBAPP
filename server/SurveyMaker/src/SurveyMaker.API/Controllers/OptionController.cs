using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyMaker.API.Models.Requests;
using SurveyMaker.Application.Features.CreateOption;
using SurveyMaker.Application.Models.Dtos;

namespace SurveyMaker.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType<OptionDto>(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateOptionRequest request)
        {
            var result = await _mediator.Send(new CreateOptionCommand
            {
                QuestionId = request.QuestionId,
                Text = request.Text
            });

            return Created(Request.Path, result);
        }
    }
}
