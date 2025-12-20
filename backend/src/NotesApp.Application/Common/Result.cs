namespace NotesApp.Application.Common;

public class Result
{
    public bool Success { get; set; }
    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }

    protected Result(bool success, string errorCode, string errorMessage)
    {
        Success = success;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public static Result Ok() => new(true, default!, default!);
    public static Result Fail(string errorCode, string errorMessage)
        => new(false, errorCode, errorMessage);
}

public class Result<T> : Result
{
    public T Value { get; }

    protected Result(bool success, T value, string errorCode, string errorMessage)
        : base(success, errorCode, errorMessage)
    {
        Value = value;
    }

    public static Result<T> Ok(T value) => new(true, value, default!, default!);
    public static new Result<T> Fail(string errorCode, string errorMessage)
        => new(false, default!, errorCode, errorMessage);
}

