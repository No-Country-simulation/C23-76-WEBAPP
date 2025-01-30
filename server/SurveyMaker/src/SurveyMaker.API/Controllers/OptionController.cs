using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyMaker.API.Models.Requests;
using SurveyMaker.Application.Features.CreateOption;
using SurveyMaker.Application.Features.DeleteOption;
using SurveyMaker.Application.Features.UpdateOption;
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

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType<OptionDto>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int id, [FromBody] CreateSurveyOptionRequest request)
        {
            var result = await _mediator.Send(new UpdateOptionCommand
            {
                Id = id,
                Text = request.Text
            });

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(string id)
        {
            var idInteger = int.Parse(id);

            var result = await _mediator.Send(new DeleteOptionCommand
            {
                Id = idInteger
            });

            return NoContent();
        }
    }
}
