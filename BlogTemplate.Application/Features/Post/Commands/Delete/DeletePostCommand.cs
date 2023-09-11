using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.User;
using BlogTemplate.Application.Features.Comments.Commands.Delete;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Application.Features.Post.Commands.Delete
{
    public class DeletePostCommand : IRequest<Result<DeletePostCommandResponse>>
    {
        public int PostId { get; set; }
        public string? DeletedByUserName { get; set; }
    }
}
