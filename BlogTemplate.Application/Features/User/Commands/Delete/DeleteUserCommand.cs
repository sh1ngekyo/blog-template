using BlogTemplate.Application.Abstractions;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Application.Features.User.Commands.Delete
{
    public class DeleteUserCommand : IRequest<Result<DeleteUserCommandResponse>>
    {
        public string? Id { get; set; }
    }
}
