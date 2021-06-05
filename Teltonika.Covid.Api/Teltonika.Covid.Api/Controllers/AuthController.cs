using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Teltonika.Covid.Api.Models;
using Teltonika.Covid.Api.Services;

namespace Teltonika.Covid.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AuthController : CovidControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<ActionResult<Token>> GetTokenAsync(CredentialsRequestModel credentialsRequest)
        {
            return ExecuteAsync(async () => await _userService.GetAccessToken(credentialsRequest));
        }
    }
}
