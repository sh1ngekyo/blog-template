using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.Post;

using MediatR;

namespace BlogTemplate.Application.Features.Post.Queries.GetAllForCurrentUser
{
    public class GetAllPostsForCurrentUserQuery : IRequest<Result<List<PostDto>>>
    {
        public string? UserName { get; set; }
    }
}
