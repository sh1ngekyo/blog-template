using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Domain.Models;
using MediatR;
using BlogTemplate.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogTemplate.Application.Features.Post.Commands.Delete
{
    public class DeletePostCommandHandler 
        : IRequestHandler<DeletePostCommand, Result<DeletePostCommandResponse>>
    {
        private readonly IUserManagerProxy<ApplicationUser> _userManager;
        private readonly IApplicationDbContext _context;
        public DeletePostCommandHandler(IUserManagerProxy<ApplicationUser> userManager, IApplicationDbContext context) =>
            (_userManager, _context) = (userManager, context);

        public async Task<Result<DeletePostCommandResponse>> Handle(DeletePostCommand request,
            CancellationToken cancellationToken)
        {
            var post = await _context.Posts!.FirstOrDefaultAsync(x => x.Id == request.PostId);
            if (post == null)
            {
                return new Result<DeletePostCommandResponse>(ErrorType.NotFound, "Post not found");
            }

            var loggedInUser = await _userManager.FindByUserNameAsync(request.DeletedByUserName!);
            var loggedInUserRole = await _userManager.GetRolesAsync(loggedInUser!);
            var response = new Result<DeletePostCommandResponse>();
            if (loggedInUserRole[0] == WebsiteRoles.WebsiteAdmin || loggedInUser?.Id == post?.ApplicationUserId)
            {
                
                _context.Posts!.Remove(post!);
                var commentsToDelete = await _context.Comments!.Where(x => x.PostId == post!.Id).ToListAsync();
                _context.Comments!.RemoveRange(commentsToDelete);
                await _context.SaveChangesAsync(cancellationToken);
                return new Result<DeletePostCommandResponse>(ResultType.Deleted).SetOutput(new DeletePostCommandResponse
                {
                    RemoveThumbnailUrl = post!.ThumbnailUrl
                });;
            }

            return new Result<DeletePostCommandResponse>(ErrorType.NotAuthorized, "Not authorized");
        }
    }
}
