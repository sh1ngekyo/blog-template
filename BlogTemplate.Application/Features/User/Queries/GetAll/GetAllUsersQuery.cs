using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.User;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Application.Features.User.Queries.GetAll
{
    public class GetAllUsersQuery : IRequest<Result<List<UserDto>>>
    {
    }
}
