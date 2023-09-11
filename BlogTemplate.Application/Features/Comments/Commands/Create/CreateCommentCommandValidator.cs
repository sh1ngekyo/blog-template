using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Application.Features.Comments.Commands.Create
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.PostId).NotNull().NotEmpty();
            RuleFor(x => x.ApplicationUserId).NotNull().NotEmpty();
            RuleFor(x => x.Content).NotNull().NotEmpty();
        }
    }
}
