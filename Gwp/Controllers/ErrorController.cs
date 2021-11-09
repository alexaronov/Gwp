using System.Globalization;
using System.Threading.Tasks;
using Gwp.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Gwp.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (context.Error is ApiException error)
            {
                // expose error details to client in case when ApiException is thrown
                Response.StatusCode = error.HttpStatusCode;
                return Problem(title: error.Message,
                               statusCode: error.HttpStatusCode,
                               type: error.ExceptionCode.ToString(CultureInfo.InvariantCulture));
            }
            return Problem(title: "Internal server error", statusCode: 500);
        }
    }
}