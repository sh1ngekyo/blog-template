using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Enums;

namespace BlogTemplate.Application.Common.Exceptions;

public class AppException : Exception
{
    public ErrorType ErrorType { get; private set; }

    public AppException(ErrorType errorType) : base(errorType.ToString())
    {
        ErrorType = errorType;
    }

    public AppException(ErrorType errorType, string errorMessage, Exception exception) : base(errorMessage, exception)
    {
        ErrorType = errorType;
    }

    public Result AsResultObject()
    {
        return new Result(ErrorType, Message, InnerException!);
    }

}
