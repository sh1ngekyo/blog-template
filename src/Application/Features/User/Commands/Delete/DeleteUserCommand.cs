using BlogTemplate.Application.Abstractions;
using MediatR;

namespace BlogTemplate.Application.Features.User.Commands.Delete
{
    public class DeleteUserCommand : IRequest<Result<DeleteUserCommandResponse>>
    {
        public string? Id { get; set; }
    }
}
