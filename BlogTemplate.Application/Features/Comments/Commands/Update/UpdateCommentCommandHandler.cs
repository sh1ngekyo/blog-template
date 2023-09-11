using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Abstractions.Enums;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace BlogTemplate.Application.Features.Comments.Commands.Update
{
    public class UpdateCommentCommandHandler 
        : IRequestHandler<UpdateCommentCommand, Result<CommentsBaseCommandResponse>>
    {
        private readonly IApplicationDbContext _context;
        public UpdateCommentCommandHandler(IApplicationDbContext context) =>
            _context = context;

        public async Task<Result<CommentsBaseCommandResponse>> Handle(UpdateCommentCommand request,
            CancellationToken cancellationToken)
        {
            var comment = await _context.Comments!.FirstOrDefaultAsync(x => x.CommentId == request.CommentId);
            if(comment == null)
            {
                return new Result<CommentsBaseCommandResponse>(ErrorType.NotFound);
            }
            comment.Content = request.Content!;
            comment.DateModified = DateTimeOffset.Now;
            _context.Comments!.Update(comment);
            await _context.SaveChangesAsync(cancellationToken);
            return new Result<CommentsBaseCommandResponse>(ResultType.Updated).SetOutput(new CommentsBaseCommandResponse
            {
                PostSlug = _context.Posts!.FirstOrDefault(x => x.Id == comment.PostId)!.Slug!,
            });
        }
    }
}
