using FluentValidation;

namespace BlogTemplate.Application.Features.Settings.Commands.Update
{
    public class UpdateSettingsCommandValidator : AbstractValidator<UpdateSettingsCommand>
    {
        public UpdateSettingsCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.SiteName).NotNull().NotEmpty();
        }
    }
}
