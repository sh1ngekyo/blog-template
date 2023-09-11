using FluentValidation;

namespace BlogTemplate.Application.Features.Post.Commands.Update
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty();
        }
    }
}
