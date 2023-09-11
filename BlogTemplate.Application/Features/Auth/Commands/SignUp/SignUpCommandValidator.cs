using FluentValidation;

namespace BlogTemplate.Application.Features.Auth.Commands.SignUp
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty();
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}
