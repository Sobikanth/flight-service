// using Application.Common.Exceptions;
// using Application.Common.Models;

using Microsoft.AspNetCore.Diagnostics;

namespace WebApi.Infrastructure;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<CustomExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is NotFoundException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            _logger.LogError(exception, "Not found exception occurred");
            await httpContext.Response.WriteAsJsonAsync(new { message = exception.Message }, cancellationToken);
        }
        else if (exception is BadHttpRequestException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            _logger.LogError(exception, "Bad request exception occurred");
            await httpContext.Response.WriteAsJsonAsync(new { message = exception.Message }, cancellationToken);
        }
        else if (exception is UnauthorizedAccessException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            _logger.LogError(exception, "Unauthorized access");
            await httpContext.Response.WriteAsJsonAsync(new { message = "Unauthorized" }, cancellationToken);
        }
        else if (exception is NotImplementedException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status501NotImplemented;
            _logger.LogError(exception, "Not implemented exception occurred");
            await httpContext.Response.WriteAsJsonAsync(new { message = exception.Message }, cancellationToken);
        }
        else
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            _logger.LogError(exception, "An error occurred while processing your request");
            await httpContext.Response.WriteAsJsonAsync(new { message = "An error occurred while processing your request." }, cancellationToken);
        }
        return true;
    }
}