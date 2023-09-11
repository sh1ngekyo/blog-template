using FluentValidation;

namespace BlogTemplate.Application.Features.Comments.Queries.GetAllByPostId
{
    public class GetAllCommentsByPostIdQueryValidator : AbstractValidator<GetAllCommentsByPostIdQuery>
    {
        public GetAllCommentsByPostIdQueryValidator()
        {
            RuleFor(x => x.PostId).NotNull().NotEmpty();
        }
    }
}
