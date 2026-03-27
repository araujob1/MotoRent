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
        if (exception is MotoRentException motoRentException)
            await HandleProjectException(motoRentException, httpContext, cancellationToken);
        else
            await ThrowUnknowException(httpContext, cancellationToken);

        return true;
    }

    private static async Task HandleProjectException(
        MotoRentException motoRentException,
        HttpContext context,
        CancellationToken cancellationToken)
    {
        context.Response.StatusCode = (int)motoRentException.StatusCode;
        await context.Response.WriteAsJsonAsync(new ErrorResponse(motoRentException.Errors), cancellationToken);
    }

    private static async Task ThrowUnknowException(HttpContext context, CancellationToken cancellationToken)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new ErrorResponse([ErrorMessages.UNKNOWN_ERROR]), cancellationToken);
    }
}
