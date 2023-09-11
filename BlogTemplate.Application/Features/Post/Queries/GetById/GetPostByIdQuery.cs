using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.Post;
using MediatR;

namespace BlogTemplate.Application.Features.Post.Queries.GetById
{
    public class GetPostByIdQuery : IRequest<Result<PostDto>>
    {
        public int Id { get; set; }
    }
}
