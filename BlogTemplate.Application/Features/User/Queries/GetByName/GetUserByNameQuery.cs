using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.Profile;
using BlogTemplate.Application.DataTransfer.User;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Application.Features.User.Queries.GetByName
{
    public class GetUserByNameQuery : IRequest<Result<UserDto>>
    {
        public string? UserName { get; set; }
    }
}
