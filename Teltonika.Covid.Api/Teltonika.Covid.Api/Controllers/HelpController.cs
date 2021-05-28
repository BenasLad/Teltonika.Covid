using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Teltonika.Covid.Api.Controllers
{
    [ApiController]
    public class HelpController : ControllerBase
    {
        [HttpGet("help")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> GetPoliciesAsync()
        {
            var helpMessage = "Please get some help dialing 1808";
            return Ok(helpMessage);
        }
    }
}
