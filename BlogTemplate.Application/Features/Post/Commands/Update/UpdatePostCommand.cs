using BlogTemplate.Application.Abstractions;
using MediatR;

namespace BlogTemplate.Application.Features.Post.Commands.Update
{
    public class UpdatePostCommand : IRequest<Result<UpdatePostCommandResponse>>
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
    }
}
