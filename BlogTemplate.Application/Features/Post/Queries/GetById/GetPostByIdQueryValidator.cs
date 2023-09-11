using FluentValidation;

namespace BlogTemplate.Application.Features.Post.Queries.GetById
{
    public class GetPostByIdQueryValidator : AbstractValidator<GetPostByIdQuery>
    {
        public GetPostByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
