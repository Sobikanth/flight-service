using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Infrastructure;

public class CustomExceptionHandler : IExceptionHandler
{
    private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

    public CustomExceptionHandler()
    {
        _exceptionHandlers = new()
            {
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
                { typeof(BadHttpRequestException),HandleBadHttpRequestException },
            };
    }

    private async Task HandleBadHttpRequestException(HttpContext context, Exception exception)
    {
        var badHttpRequestException = (BadHttpRequestException)exception;

        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        await context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Detail = badHttpRequestException.Message
        });
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var exceptionType = exception.GetType();

        if (_exceptionHandlers.ContainsKey(exceptionType))
        {
            await _exceptionHandlers[exceptionType].Invoke(httpContext, exception);
            return true;
        }

        return false;
    }

    private async Task HandleNotFoundException(HttpContext httpContext, Exception ex)
    {
        var exception = (NotFoundException)ex;

        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails()
        {
            Status = StatusCodes.Status404NotFound,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception.Message
        });
    }

    private async Task HandleUnauthorizedAccessException(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
        });
    }
}