using System.Net;

namespace MotoRent.Exceptions;

public sealed class ErrorOnValidationException(IReadOnlyList<string> errorMessages) : MotoRentException(string.Empty)
{
    public override IReadOnlyList<string> Errors { get; } = errorMessages;

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
