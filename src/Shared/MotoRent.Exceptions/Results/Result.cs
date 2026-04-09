namespace MotoRent.Exceptions;

public sealed class Result<T>
{
    public T? Value { get; }
    public MotoRentError? Error { get; }

    public bool IsSuccess => Error is null;
    public bool IsFailure => !IsSuccess;

    private Result(T value)
    {
        Value = value;
    }

    private Result(MotoRentError error)
    {
        Error = error;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(MotoRentError error) => new(error);
}
