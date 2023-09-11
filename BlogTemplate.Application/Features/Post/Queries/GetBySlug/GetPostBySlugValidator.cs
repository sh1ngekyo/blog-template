using FluentValidation;

namespace BlogTemplate.Application.Features.Post.Queries.GetBySlug
{
    public class GetPostBySlugValidator : AbstractValidator<GetPostBySlugQuery>
    {
        public GetPostBySlugValidator()
        {
            RuleFor(x => x.Slug).NotNull().NotEmpty();
        }
    }
}
