using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Enums;

using FluentValidation;
using FluentValidation.Results;

using MediatR;

namespace BlogTemplate.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result, new()
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) =>
            _validators = validators;

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                List<ValidationFailure> failures = _validators
                    .Select(v => v.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Any())
                {
                    TResponse response = new TResponse();

                    response.Set(ErrorType.NotValid, failures.Select(s => s.ErrorMessage), null);

                    return Task.FromResult<TResponse>(response);
                }
                else
                {
                    return next();
                }
            }

            return next();
        }
    }
}
