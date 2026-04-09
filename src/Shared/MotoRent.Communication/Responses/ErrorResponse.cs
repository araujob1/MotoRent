namespace MotoRent.Communication.Responses;

public sealed record ErrorResponse(string Code, int StatusCode, IReadOnlyList<string> Errors);
