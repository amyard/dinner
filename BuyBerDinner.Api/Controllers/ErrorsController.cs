using BuyBerDinner.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuyBerDinner.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statusCode, message) = exception switch
        {
            IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
        };
        
        // ProblemDetail.Type - type url will change depends on status code.
        // return Problem(title: exception?.Message, statusCode: 400);
        return Problem(statusCode: statusCode, title: message);
    }
}