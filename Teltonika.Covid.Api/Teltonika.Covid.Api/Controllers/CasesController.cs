using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Teltonika.Covid.Api.Models;
using Teltonika.Covid.Api.Repositories;
using Teltonika.Covid.Api.Services;

namespace Teltonika.Covid.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CasesController : CovidControllerBase
    {
        private readonly ICaseService _caseService;

        public CasesController(ICaseService caseService)
        {
            _caseService = caseService;
        }

        [AllowAnonymous]
        [HttpGet("metadata")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<ActionResult<ListMetadata>> GetListMetadata()
        {
            return ExecuteAsync(async () => await _caseService.GetListMetadata());
        }

        [AllowAnonymous]
        [HttpPost("cases/list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<ActionResult<GetCasesResponse>> GetCasesAsync(ListOptions listOptions)
        {
            return ExecuteAsync(async () => await _caseService.GetCasesAsync(listOptions));
        }

        [HttpPost("cases")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public Task<ActionResult<int>> CreateCaseAsync(CreateCaseRequest caseToCreate)
        {
            return CreateAsync(async () => await _caseService.CreateCaseAsync(caseToCreate));
        }
    }
}
