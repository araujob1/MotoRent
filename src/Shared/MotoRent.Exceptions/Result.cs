namespace MotoRent.Exceptions;

public sealed class Result<T>
{
    public T? Value { get; }
    public MotoRentException? Error { get; }

    public bool IsSuccess => Error is null;
    public bool IsFailure => !IsSuccess;

    private Result(T value)
    {
        Value = value;
    }

    private Result(MotoRentException error)
    {
        Error = error;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(MotoRentException error) => new(error);
}

public sealed class Result
{
    public MotoRentException? Error { get; }

    public bool IsSuccess => Error is null;
    public bool IsFailure => !IsSuccess;

    private Result() { }

    private Result(MotoRentException error)
    {
        Error = error;
    }

    public static Result Success() => new();
    public static Result Failure(MotoRentException error) => new(error);
}
