using FluentValidation;

namespace BlogTemplate.Application.Features.Comments.Commands.Delete
{
    public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
    {
        public DeleteCommentCommandValidator()
        {
            RuleFor(x => x.CommentId).NotEmpty();
        }
    }
}
