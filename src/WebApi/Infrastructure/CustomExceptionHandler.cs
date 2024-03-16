using Application.Common.Exceptions;
using Application.Common.Models;

using Microsoft.AspNetCore.Diagnostics;

namespace WebApi.Infrastructure;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<CustomExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var response = new ErrorResponse();

        if (exception is NotFoundException)
        {
            response.StatusCode = StatusCodes.Status404NotFound;
            response.Title = "Resource not found";
            response.ExceptionMessage = exception.Message;
            _logger.LogError(exception, response.Title);
        }
        else if (exception is BadHttpRequestException badHttpRequestException)
        {
            response.StatusCode = StatusCodes.Status400BadRequest;
            response.Title = "Bad request";
            response.ExceptionMessage = badHttpRequestException.Message;
            _logger.LogError(exception, response.Title);
        }
        else
        {
            response.StatusCode = StatusCodes.Status500InternalServerError;
            response.Title = "An error occurred";
            response.ExceptionMessage = "An error occurred";
            _logger.LogError(exception, response.Title);
        }

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        return true;
    }
}