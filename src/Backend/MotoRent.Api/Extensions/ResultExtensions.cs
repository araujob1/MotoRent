using MotoRent.Communication.Responses;
using MotoRent.Exceptions;

namespace MotoRent.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToHttpResponse<T>(this Result<T> result, Func<T, IResult> onSuccess)
    {
        if (result.IsFailure)
            return ToErrorResponse(result.Error!);

        return onSuccess(result.Value!);
    }

    private static IResult ToErrorResponse(MotoRentError error) =>
        Results.Json(
            new ErrorResponse(error.Code, (int)error.StatusCode, error.Errors),
            statusCode: (int)error.StatusCode);
}
