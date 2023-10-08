using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.Post;

using MediatR;

namespace BlogTemplate.Application.Features.Post.Queries.GetBySlug
{
    public class GetPostBySlugQuery : IRequest<Result<PostDto>>
    {
        public string? Slug { get; set; }
    }
}
