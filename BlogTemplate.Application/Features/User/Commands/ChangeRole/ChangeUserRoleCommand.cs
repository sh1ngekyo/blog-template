using BlogTemplate.Application.Abstractions;
using MediatR;

namespace BlogTemplate.Application.Features.User.Commands.ChangeRole
{
    public class ChangeUserRoleCommand : IRequest<Result<ChangeUserRoleCommandResponse>>
    {
        public string? Id { get; set; }
        public bool IsAdmin { get; set; }
    }
}
