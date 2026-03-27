using System.Net;

namespace MotoRent.Exceptions;

public abstract class MotoRentException(string message) : Exception(message)
{
    public abstract IReadOnlyList<string> Errors { get; }

    public abstract HttpStatusCode StatusCode { get; }
}
