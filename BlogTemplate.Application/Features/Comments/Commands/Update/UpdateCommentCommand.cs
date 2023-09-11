using BlogTemplate.Application.Abstractions;
using MediatR;

namespace BlogTemplate.Application.Features.Comments.Commands.Update
{
    public class UpdateCommentCommand : IRequest<Result<CommentsBaseCommandResponse>>
    {
        public int CommentId { get; set; }
        public string? Content { get; set; }
    }
}
