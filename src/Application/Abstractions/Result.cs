using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Common.Errors;

namespace BlogTemplate.Application.Abstractions;

public class Result
{
    public bool Conclusion { get; private set; }

    public ResultType? ResultType { get; private set; }

    public ErrorDescription? ErrorDescription { get; private set; }

    public Result()
    {
        Conclusion = true;
        ResultType = Enums.ResultType.Ok;
    }

    public Result(ResultType resultType)
    {
        Conclusion = true;
        this.ResultType = resultType;
    }

    public Result(ErrorType errorType)
    {
        Conclusion = false;
        ErrorDescription = new ErrorDescription
        {
            ErrorType = errorType
        };
    }

    public Result(ErrorType errorType, Exception exception)
    {
        Conclusion = false;
        ErrorDescription = new ErrorDescription
        {
            ErrorMessage = exception.Message,
            Exception = exception,
            ErrorType = errorType
        };
    }

    public Result(ErrorType errorType, string errorMessage, Exception? exception = null)
    {
        Conclusion = false;
        ErrorDescription = new ErrorDescription
        {
            ErrorMessage = errorMessage,
            Exception = exception!,
            ErrorType = errorType
        };
    }

    public Result(ErrorType errorType, IEnumerable<string> errorMessages, Exception? exception = null)
    {
        Conclusion = false;
        ErrorDescription = new ErrorDescription
        {
            ErrorMessage = string.Join("\n", errorMessages),
            Exception = exception!,
            ErrorType = errorType
        };
    }

    public Result EnsureSuccess()
    {
        return !Conclusion ? throw ErrorDescription!.AsException() : this;
    }

    public void Set(ErrorType errorType, IEnumerable<string> errorMessages, Exception? exception = null)
    {
        Conclusion = false;
        ErrorDescription = new ErrorDescription
        {
            ErrorMessage = string.Join("\n", errorMessages),
            Exception = exception!,
            ErrorType = errorType
        };
    }
}

public class Result<T> : Result
{
    public T? Output { get; private set; }


    public Result() : base() { }
    public Result(ResultType resultType) : base(resultType) { }


    public Result(ErrorType errorType) : base(errorType) { }
    public Result(ErrorType errorType, Exception exception) : base(errorType, exception) { }
    public Result(ErrorType errorType, string errorMessage, Exception? exception = null) : base(errorType, errorMessage, exception!) { }

    public Result(ErrorDescription errorDescription) : base(errorDescription.ErrorType, errorDescription.ErrorMessage!, errorDescription.Exception) { }


    public Result<T> AddMethodInfo(params string[] infos)
    {
        base.ErrorDescription!.ErrorMessage = $"{typeof(T).Name} {string.Join(", ", infos)}, {ErrorDescription.ErrorMessage}";
        return this;
    }


    public Result<T> SetOutput(T value)
    {
        Output = value;
        return this;
    }

    public T EnsureOutput()
    {
        return Conclusion && Output != null ? Output : throw ErrorDescription!.AsException();
    }
}
