using FluentValidation;

namespace BlogTemplate.Application.Features.Home.Queries.GetByPageNumber
{
    public class GetHomeByPageNumberQueryValidator : AbstractValidator<GetHomeByPageNumberQuery>
    {
        public GetHomeByPageNumberQueryValidator()
        {
            RuleFor(x => x.Page).NotEmpty();
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}
