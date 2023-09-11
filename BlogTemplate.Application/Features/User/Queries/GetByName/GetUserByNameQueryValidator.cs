using FluentValidation;

namespace BlogTemplate.Application.Features.User.Queries.GetByName
{
    public class GetUserByNameQueryValidator : AbstractValidator<GetUserByNameQuery>
    {
        public GetUserByNameQueryValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty();
        }
    }
}
