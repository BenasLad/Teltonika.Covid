using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
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

        protected Task<ActionResult<T>> CreateAsync<T>(Func<Task<T>> func)
        {
            return ExecuteAsyncInternal<T>(async () => Created("", await func()));
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
            catch (ValidationException ex)
            {
                var problemDetails = ProblemDetailsFactory.CreateProblemDetails(HttpContext, statusCode: (int?)HttpStatusCode.BadRequest, detail: ex.Message);
                result = BadRequest(problemDetails);
            }
            return result;
        }
    }
}
