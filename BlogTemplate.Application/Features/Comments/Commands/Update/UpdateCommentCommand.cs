using BlogTemplate.Application.Abstractions;
using BlogTemplate.Domain.Models;
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Application.Features.Comments.Commands.Update
{
    public class UpdateCommentCommand : IRequest<Result<CommentsBaseCommandResponse>>
    {
        public int CommentId { get; set; }
        public string? Content { get; set; }
    }
}
