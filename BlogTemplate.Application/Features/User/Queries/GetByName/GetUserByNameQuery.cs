using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.User;

using MediatR;

namespace BlogTemplate.Application.Features.User.Queries.GetByName
{
    public class GetUserByNameQuery : IRequest<Result<UserDto>>
    {
        public string? UserName { get; set; }
    }
}
