using BlogTemplate.Application.Abstractions;

using MediatR;

namespace BlogTemplate.Application.Features.Post.Commands.Delete
{
    public class DeletePostCommand : IRequest<Result<DeletePostCommandResponse>>
    {
        public int PostId { get; set; }
        public string? DeletedByUserName { get; set; }
    }
}
