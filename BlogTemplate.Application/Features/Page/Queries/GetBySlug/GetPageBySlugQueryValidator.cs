using FluentValidation;

namespace BlogTemplate.Application.Features.Page.Queries.GetBySlug
{
    public class GetPageBySlugQueryValidator : AbstractValidator<GetPageBySlugQuery>
    {
        public GetPageBySlugQueryValidator()
        {
            RuleFor(x => x.Slug).NotNull().NotEmpty();
        }
    }
}
