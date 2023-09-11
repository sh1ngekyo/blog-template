using FluentValidation;

namespace BlogTemplate.Application.Features.Post.Commands.Delete
{
    public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(x => x.DeletedByUserName).NotNull().NotEmpty();
        }
    }
}
