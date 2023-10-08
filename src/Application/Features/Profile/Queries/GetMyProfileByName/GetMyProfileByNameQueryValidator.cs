using BlogTemplate.Application.Features.Profile.Queries.GetByName;
using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Application.Features.Profile.Queries.GetMyProfileByName
{
    public class GetMyProfileByNameQueryValidator : AbstractValidator<GetMyProfileByNameQuery>
    {
        public GetMyProfileByNameQueryValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty();
        }
    }
}
