using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.Post;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using X.PagedList;

namespace BlogTemplate.Application.Features.Post.Queries.GetAllForCurrentUser
{
    public class GetAllPostsForCurrentUserQuery : IRequest<Result<List<PostDto>>>
    {
        public string? UserName { get; set; }
    }
}
