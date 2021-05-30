using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Teltonika.Covid.Api.Exceptions;

namespace Teltonika.Covid.Api.Controllers
{
    [ApiController]
    public class CovidControllerBase : ControllerBase
    {
        protected Task<ActionResult<T>> ExecuteAsync<T>(Func<Task<T>> func)
        {
            return ExecuteAsyncInternal<T>(async () => Ok(await func()));
        }

        private async Task<ActionResult<T>> ExecuteAsyncInternal<T>(Func<Task<ActionResult<T>>> func)
        {
            ActionResult<T> result;
            try
            {
                result = await func();
            }
            catch (UnauthorizedException ex)
            {
                var problemDetails = ProblemDetailsFactory.CreateProblemDetails(HttpContext, statusCode: (int?)HttpStatusCode.Unauthorized, detail: ex.Message);
                result = Unauthorized(problemDetails);
            }
            return result;
        }
    }
}
