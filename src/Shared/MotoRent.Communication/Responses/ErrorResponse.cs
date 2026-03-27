namespace MotoRent.Communication.Responses;

public sealed record ErrorResponse
{
    public IReadOnlyList<string> Errors { get; }

    public ErrorResponse(IReadOnlyList<string> errors) => Errors = errors;

    public ErrorResponse(string error)
    {
        Errors =
        [
            error
        ];
    }
};
