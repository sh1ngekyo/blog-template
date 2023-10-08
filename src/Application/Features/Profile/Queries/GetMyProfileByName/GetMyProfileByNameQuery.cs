using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.User;

using MediatR;

namespace BlogTemplate.Application.Features.Profile.Queries.GetMyProfileByName
{
    public class GetMyProfileByNameQuery : IRequest<Result<UserDto>>
    {
        public string? UserName { get; set; }
    }
}
