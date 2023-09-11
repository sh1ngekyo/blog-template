using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Abstractions.Enums;
using MediatR;

namespace BlogTemplate.Application.Features.Comments.Commands.Create
{
    public class CreateCommentCommandHandler 
        : IRequestHandler<CreateCommentCommand, Result<CommentsBaseCommandResponse>>
    {
        private readonly IApplicationDbContext _context;
        public CreateCommentCommandHandler(IApplicationDbContext context) =>
            _context = context;

        public async Task<Result<CommentsBaseCommandResponse>> Handle(CreateCommentCommand request,
            CancellationToken cancellationToken)
        {
            var dateCreated = DateTimeOffset.Now;

            await _context.Comments!.AddAsync(new Domain.Models.Comment
            {
                ParentId = request.ParentId,
                PostId = request.PostId,
                Content = request.Content!,
                ApplicationUserId = request.ApplicationUserId,
                DateCreated = dateCreated,
                DateModified = dateCreated,
            });
            await _context.SaveChangesAsync(cancellationToken);
            return new Result<CommentsBaseCommandResponse>(ResultType.Created).SetOutput(new CommentsBaseCommandResponse
            {
                PostSlug = _context.Posts!.FirstOrDefault(x => x.Id == request.PostId)!.Slug!,
            });
        }
    }
}
