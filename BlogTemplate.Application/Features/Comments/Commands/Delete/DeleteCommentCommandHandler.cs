using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Domain.Models;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace BlogTemplate.Application.Features.Comments.Commands.Delete
{
    public class DeleteCommentCommandHandler 
        : IRequestHandler<DeleteCommentCommand, Result<CommentsBaseCommandResponse>>
    {
        private readonly IApplicationDbContext _context;
        public DeleteCommentCommandHandler(IApplicationDbContext context) =>
            _context = context;

        public async Task<Result<CommentsBaseCommandResponse>> Handle(DeleteCommentCommand request,
            CancellationToken cancellationToken)
        {
            var comments = await _context.Comments!
            .Include(x => x.Children).ToListAsync();

            var flatten = Flatten(comments.Where(x => x.CommentId == request.CommentId));

            _context.Comments!.RemoveRange(flatten);

            await _context.SaveChangesAsync(cancellationToken);

            return new Result<CommentsBaseCommandResponse>(ResultType.Deleted).SetOutput(
                new CommentsBaseCommandResponse
                {
                    PostSlug = _context.Posts!.FirstOrDefault(x => x.Id == flatten.FirstOrDefault()!.PostId)!.Slug!
                });
        }
        IEnumerable<Comment> Flatten(IEnumerable<Comment> comments) =>
            comments.SelectMany(x => Flatten(x.Children)).Concat(comments);
    }
}
