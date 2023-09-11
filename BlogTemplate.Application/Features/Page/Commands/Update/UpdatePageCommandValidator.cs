using FluentValidation;

namespace BlogTemplate.Application.Features.Page.Commands.Update
{
    public class UpdatePageCommandValidator : AbstractValidator<UpdatePageCommand>
    {
        public UpdatePageCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Page must be with title!");
        }
    }
}
