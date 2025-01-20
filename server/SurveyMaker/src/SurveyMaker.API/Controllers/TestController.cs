using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SurveyMaker.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TestController : ControllerBase
    {
        [Authorize]
        [HttpGet("Private")]
        public async Task<IActionResult> GetPrivate()
        {
            await Task.CompletedTask;
            return Ok(new
            {
                Message = "You Have Acces to te private endpoints."
            });
        }

        [HttpGet("Public")]
        public async Task<IActionResult> GetPublic()
        {
            await Task.CompletedTask;
            return Ok(new
            {
                Message = "Meh, just a public endpoint."
            });
        }
    }
}
