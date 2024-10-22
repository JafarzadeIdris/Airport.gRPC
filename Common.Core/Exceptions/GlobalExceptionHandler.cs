using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Common.Core.Exceptions
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, exception.Message);
            (string Details, string Title, int StatusCode) = exception switch
            {
                ArgumentException =>
                (exception.StackTrace ?? "No stack trace available", exception.Message, httpContext.Response.StatusCode = StatusCodes.Status404NotFound),
                ValidationException =>
                (exception.StackTrace ?? "No stack trace available", exception.Message, httpContext.Response.StatusCode = StatusCodes.Status400BadRequest),
                _ =>
                (exception.StackTrace ?? "No stack trace available", exception.Message, httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError)
            };

            ProblemDetails problemDetails = new ()
            {
                Detail = Details,
                Title = Title,
                Status = StatusCode
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails,cancellationToken);
            return true;
        }
    }
}
