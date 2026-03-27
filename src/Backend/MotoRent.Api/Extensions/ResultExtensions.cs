using MotoRent.Communication.Responses;
using MotoRent.Exceptions;

namespace MotoRent.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToHttpResponse<T>(this Result<T> result, Func<T, IResult> onSuccess)
    {
        if (result.IsFailure)
            return Results.Json(
                new ErrorResponse(result.Error!.Errors),
                statusCode: (int)result.Error.StatusCode);

        return onSuccess(result.Value!);
    }

    public static IResult ToHttpResponse(this Result result, Func<IResult> onSuccess)
    {
        if (result.IsFailure)
            return Results.Json(
                new ErrorResponse(result.Error!.Errors),
                statusCode: (int)result.Error.StatusCode);

        return onSuccess();
    }
}
