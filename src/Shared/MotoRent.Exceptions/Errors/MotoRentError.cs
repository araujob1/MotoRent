using System.Net;

namespace MotoRent.Exceptions;

public sealed record MotoRentError(string Code, HttpStatusCode StatusCode, IReadOnlyList<string> Errors)
{
    public static MotoRentError Validation(IReadOnlyList<string> errors) =>
        new("validation_error", HttpStatusCode.BadRequest, [.. errors]);

    public static MotoRentError BadRequest(string error) =>
        new("bad_request", HttpStatusCode.BadRequest, [error]);

    public static MotoRentError Unknown() =>
        new("unknown_error", HttpStatusCode.InternalServerError, [ErrorMessages.UNKNOWN_ERROR]);
}
