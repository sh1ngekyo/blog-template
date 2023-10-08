using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogTemplate.Application.Features.Post.Commands.Update
{
    public class UpdatePostCommandHandler 
        : IRequestHandler<UpdatePostCommand, Result<UpdatePostCommandResponse>>
    {
        private readonly IApplicationDbContext _context;
        public UpdatePostCommandHandler(IApplicationDbContext context) =>
             _context = context;

        public async Task<Result<UpdatePostCommandResponse>> Handle(UpdatePostCommand request,
            CancellationToken cancellationToken)
        {
            var post = await _context.Posts!.FirstOrDefaultAsync(x => x.Id == request.PostId);
            if (post == null)
            {
                return new Result<UpdatePostCommandResponse>(ErrorType.NotFound, "Post not found");
            }

            var response = new UpdatePostCommandResponse();
            post.Title = request.Title;
            post.ShortDescription = request.ShortDescription;
            post.Description = request.Description; 
            if (request.ThumbnailUrl != null)
            {
                response.RemoveThumbnailUrl = post.ThumbnailUrl;
                post.ThumbnailUrl = request.ThumbnailUrl;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new Result<UpdatePostCommandResponse>(ResultType.Updated).SetOutput(response);
        }
    }
}
