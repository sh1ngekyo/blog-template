using AutoMapper;
using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.Post;
using MediatR;
using BlogTemplate.Domain.Models;
using BlogTemplate.Domain;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace BlogTemplate.Application.Features.Post.Queries.GetAllForCurrentUser
{
    public class GetAllPostsForCurrentUserQueryHandler 
        : IRequestHandler<GetAllPostsForCurrentUserQuery, Result<List<PostDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserManagerProxy<ApplicationUser> _userManager;
        public GetAllPostsForCurrentUserQueryHandler(IUserManagerProxy<ApplicationUser> userManager, IApplicationDbContext context, IMapper mapper) =>
            (_context, _mapper, _userManager) = (context, mapper, userManager);

        public async Task<Result<List<PostDto>>> Handle(GetAllPostsForCurrentUserQuery request,
            CancellationToken cancellationToken)
        {
            var loggedInUser = await _userManager.FindByUserNameAsync(request.UserName!);
            var loggedInUserRole = (await _userManager.GetRolesAsync(loggedInUser!)).FirstOrDefault();
            var listOfPosts = loggedInUserRole == WebsiteRoles.WebsiteAdmin ?
                await _context.Posts!
                .Include(x => x.ApplicationUser)
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .ToListAsync() :
                await _context.Posts!
                .Include(x => x.ApplicationUser)
                .Where(x => x.ApplicationUserId == loggedInUser!.Id)
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return new Result<List<PostDto>>(ResultType.Ok).SetOutput(listOfPosts);
        }
    }
}
