using FluentValidation;

namespace BlogTemplate.Application.Features.Profile.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty();
            RuleFor(x => x.NewPassword).NotNull().NotEmpty();
        }
    }
}
