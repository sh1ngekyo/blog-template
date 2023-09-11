using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.Post;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Application.Features.Post.Queries.GetBySlug
{
    public class GetPostBySlugQuery : IRequest<Result<PostDto>>
    {
        public string? Slug { get; set; }
    }
}
