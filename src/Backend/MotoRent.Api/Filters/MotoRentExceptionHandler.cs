using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using MotoRent.Communication.Responses;
using MotoRent.Exceptions;

namespace MotoRent.Api.Filters;

public sealed class MotoRentExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var error = exception switch
        {
            BadHttpRequestException => MotoRentError.BadRequest(ErrorMessages.INVALID_REQUEST),
            JsonException => MotoRentError.BadRequest(ErrorMessages.INVALID_REQUEST),
            _ => MotoRentError.Unknown()
        };

        await WriteErrorResponse(error, httpContext, cancellationToken);

        return true;
    }

    private static async Task WriteErrorResponse(
        MotoRentError error,
        HttpContext context,
        CancellationToken cancellationToken)
    {
        context.Response.StatusCode = (int)error.StatusCode;
        await context.Response.WriteAsJsonAsync(
            new ErrorResponse(error.Code, (int)error.StatusCode, error.Errors),
            cancellationToken);
    }
}
