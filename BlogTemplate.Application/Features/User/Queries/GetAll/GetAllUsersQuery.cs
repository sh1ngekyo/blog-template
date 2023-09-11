using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.User;
using MediatR;

namespace BlogTemplate.Application.Features.User.Queries.GetAll
{
    public class GetAllUsersQuery : IRequest<Result<List<UserDto>>>
    {
    }
}
