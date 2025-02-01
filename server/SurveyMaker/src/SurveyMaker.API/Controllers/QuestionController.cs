using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyMaker.API.Models.Requests;
using SurveyMaker.Application.Features.CreateQuestion;
using SurveyMaker.Application.Features.CreateSurvey;
using SurveyMaker.Application.Features.DeleteQuestion;
using SurveyMaker.Application.Features.UpdateQuestion;
using SurveyMaker.Application.Models.Dtos;

namespace SurveyMaker.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Authorize]
        [ProducesResponseType<QuestionDto>(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateQuestionRequest request)
        {
            var result = await _mediator.Send(new CreateQuestionCommand
            {
                SurveyId = request.SurveyId,
                Title = request.Title,
                Type = request.Type,
                MaxSelections = request.MaxSelections,
                Options = request.Options.Select(y => new CreateSurveyOptionDto
                {
                    Text = y.Text,
                }).ToList()
            });

            return Created(Request.Path, result);
        }


        [HttpPut("{questionId}")]
        [Authorize]
        [ProducesResponseType<QuestionDto>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] int questionId, [FromBody] UpdateQuestionRequest request)
        {
            var result = await _mediator.Send(new UpdateQuestionCommand
            {
                Id = questionId,
                SurveyId = request.SurveyId,
                Title = request.Title,
                MaxSelections = request.MaxSelections
            });

            return Ok(result);
        }

        [HttpDelete("{questionId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(string questionId)
        {
            var id = int.Parse(questionId);
            await _mediator.Send(new DeleteQuestionCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}
