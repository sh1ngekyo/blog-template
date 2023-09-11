using BlogTemplate.Application.Abstractions;

using MediatR;

namespace BlogTemplate.Application.Features.Post.Commands.Create
{
    public class CreatePostCommand : IRequest<Result>
    {
        public string? UserName { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
    }
}
