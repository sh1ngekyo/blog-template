using FluentValidation;

namespace BlogTemplate.Application.Features.Profile.Queries.GetByName
{
    public class GetProfileByUserNameQueryValidator : AbstractValidator<GetProfileByUserNameQuery>
    {
        public GetProfileByUserNameQueryValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty();
        }
    }
}
