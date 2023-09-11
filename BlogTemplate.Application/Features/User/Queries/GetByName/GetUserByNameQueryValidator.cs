using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
