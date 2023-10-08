using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Domain.Models;
using MediatR;
using BlogTemplate.Application.Abstractions.Database;

namespace BlogTemplate.Application.Features.Post.Commands.Create
{
    public class CreatePostCommandHandler 
        : IRequestHandler<CreatePostCommand, Result>
    {
        private readonly IUserManagerProxy<ApplicationUser> _userManager;
        private readonly IApplicationDbContext _context;
        public CreatePostCommandHandler(IUserManagerProxy<ApplicationUser> userManager, IApplicationDbContext context) =>
            (_userManager, _context) = (userManager, context);

        public async Task<Result> Handle(CreatePostCommand request,
            CancellationToken cancellationToken)
        {
            var loggedInUser = await _userManager.FindByUserNameAsync(request.UserName!);

            var post = new Domain.Models.Post();

            post.Title = request.Title;
            post.Description = request.Description;
            post.ShortDescription = request.ShortDescription;
            post.ApplicationUserId = loggedInUser!.Id;
            post.ThumbnailUrl = request.ThumbnailUrl;

            if (post.Title != null)
            {
                string slug = request.Title!.Trim();
                slug = slug.Replace(" ", "-");
                post.Slug = slug + "-" + Guid.NewGuid();
            } 

            await _context.Posts!.AddAsync(post);
            await _context.SaveChangesAsync(cancellationToken);

            return new Result(ResultType.Created);
        }
    }
}
