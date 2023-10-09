using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Common.Exceptions;

namespace BlogTemplate.Application.Common.Errors;

public class ErrorDescription
{
    public ErrorType ErrorType { get; set; }
    public string? ErrorMessage { get; set; }
    public string? StackTrace { get; set; }

    private Exception? _exception;
    internal Exception Exception
    {
        get => _exception!;
        set
        {
            _exception = value;
            StackTrace = _exception?.StackTrace!;
        }
    }

    public Guid Guid { get; private set; }

    public ErrorDescription()
    {
        Guid = Guid.NewGuid();
    }

    public AppException AsException()
    {
        return new AppException(ErrorType, ErrorMessage!, Exception);
    }
}
