using Application.Common.Models;

using Microsoft.AspNetCore.Diagnostics;

using WebApi.Common.Exceptions;

namespace WebApi.Infrastructure;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    // private readonly ILogger<CustomExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var response = new ErrorResponse();

        if (exception is NotFoundException)
        {
            response.StatusCode = StatusCodes.Status404NotFound;
            response.Title = "Resource not found";
            response.ExceptionMessage = exception.Message;
        }
        else if (exception is BadHttpRequestException badHttpRequestException)
        {
            response.StatusCode = StatusCodes.Status400BadRequest;
            response.Title = "Bad request";
            response.ExceptionMessage = badHttpRequestException.Message;
        }
        else
        {
            response.StatusCode = StatusCodes.Status500InternalServerError;
            response.Title = "An error occurred";
            response.ExceptionMessage = "An error occurred";
        }

        // Log.Error(exception, "An error occurred");
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        return true;
    }
}