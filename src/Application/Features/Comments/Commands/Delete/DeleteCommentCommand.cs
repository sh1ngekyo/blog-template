using BlogTemplate.Application.Abstractions;

using MediatR;

namespace BlogTemplate.Application.Features.Comments.Commands.Delete
{
    public class DeleteCommentCommand : IRequest<Result<CommentsBaseCommandResponse>>
    {
        public int CommentId { get; set; }
    }
}
