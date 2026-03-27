using System.Net;

namespace MotoRent.Exceptions;

public sealed class NotFoundException(string message) : MotoRentException(message)
{
    public override IReadOnlyList<string> Errors => [Message];

    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}
