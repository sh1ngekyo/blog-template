using BlogTemplate.Application.Abstractions;
using MediatR;

namespace BlogTemplate.Application.Features.Comments.Commands.Create
{
    public class CreateCommentCommand : IRequest<Result<CommentsBaseCommandResponse>>
    {
        public int? ParentId { get; set; }
        public string? Content { get; set; }
        public int PostId { get; set; }
        public string? ApplicationUserId { get; set; }
    }
}
