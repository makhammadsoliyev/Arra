using Arra.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Arra.Api.Infrastructure;

internal sealed class ValidationExceptionHandler
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is ValidationException validationException)
        {
            var problemDetails = new ProblemDetails
            {
                Title = validationException.Code,
                Status = StatusCodes.Status400BadRequest,
                Detail = validationException.Message,
                Instance = httpContext.Request.Path,
                Type = "https://httpstatuses.com/400",
                Extensions =
                {
                    ["errors"] = validationException.Errors
                }
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        return false;
    }
}