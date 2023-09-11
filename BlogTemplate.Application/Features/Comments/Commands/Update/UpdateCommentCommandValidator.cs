using BlogTemplate.Application.Features.Comments.Commands.Create;
using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Application.Features.Comments.Commands.Update
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(x => x.CommentId).NotEmpty();
            RuleFor(x => x.Content).NotNull().NotEmpty();
        }
    }
}
