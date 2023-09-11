using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
