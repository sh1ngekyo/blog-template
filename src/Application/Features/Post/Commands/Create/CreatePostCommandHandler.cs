using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Domain.Models;
using MediatR;

namespace BlogTemplate.Application.Features.Post.Commands.Create;

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

        var post = new Domain.Models.Post
        {
            Title = request.Title,
            Description = request.Description,
            ShortDescription = request.ShortDescription,
            ApplicationUserId = loggedInUser!.Id,
            ThumbnailUrl = request.ThumbnailUrl
        };

        if (post.Title != null)
        {
            string slug = request.Title!.Trim();
            slug = slug.Replace(" ", "-");
            post.Slug = slug + "-" + Guid.NewGuid();
        }

        await _context.Posts!.AddAsync(post, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result(ResultType.Created);
    }
}
