namespace EnglishLearningSystem.Domain.Common.Results;

public interface IResultFactory<TSelf> { static abstract TSelf Failure(Error error); }

/// <summary>Result for commands that return no value.</summary>
public sealed class Result : IResultFactory<Result>
{
    private static readonly Result SuccessInstance = new(null);

    public bool IsSuccess => Error is null;
    public Error? Error { get; }

    private Result(Error? error) { Error = error; }

    public static Result Success() => SuccessInstance;
    public static Result Failure(Error error) => new(error);
    public static implicit operator Result(Error error) => new(error);
}

public sealed class Result<T> : IResultFactory<Result<T>>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public Error? Error { get; }

    private Result(T value) { IsSuccess = true; Value = value; }

    private Result(Error error) { Error = error; }

    public static Result<T> Failure(Error error) => new(error);
    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Error error) => new(error);
}