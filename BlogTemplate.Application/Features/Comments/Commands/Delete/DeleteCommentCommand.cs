using BlogTemplate.Application.Abstractions;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Application.Features.Comments.Commands.Delete
{
    public class DeleteCommentCommand : IRequest<Result<CommentsBaseCommandResponse>>
    {
        public int CommentId { get; set; }
    }
}
