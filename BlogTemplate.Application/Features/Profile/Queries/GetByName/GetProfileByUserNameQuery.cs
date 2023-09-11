using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.Post;
using BlogTemplate.Application.DataTransfer.Profile;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Application.Features.Profile.Queries.GetByName
{
    public class GetProfileByUserNameQuery : IRequest<Result<ProfileDto>>
    {
        public string? UserName { get; set; }
        public int? Page { get; set; }
    }
}
