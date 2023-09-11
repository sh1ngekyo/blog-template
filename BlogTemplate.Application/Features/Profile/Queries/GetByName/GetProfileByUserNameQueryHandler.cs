using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.DataTransfer.Profile;
using BlogTemplate.Domain.Models;

using MediatR;
using Microsoft.EntityFrameworkCore;

using X.PagedList;

namespace BlogTemplate.Application.Features.Profile.Queries.GetByName
{
    public class GetProfileByUserNameQueryHandler 
        : IRequestHandler<GetProfileByUserNameQuery, Result<ProfileDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserManagerProxy<ApplicationUser> _userManager;
        public GetProfileByUserNameQueryHandler(IApplicationDbContext context, IUserManagerProxy<ApplicationUser> userManager) =>
            (_context, _userManager) = (context, userManager);

        public async Task<Result<ProfileDto>> Handle(GetProfileByUserNameQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByUserNameAsync(request.UserName!);
            if(user == null)
            {
                return new Result<ProfileDto>(ErrorType.NotFound);
            }
            var profileDto = new ProfileDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                About = user.About,
                UserPicUrl = user.ThumbnailUrl,
            };
            var setting = _context.Settings!.ToList();
            profileDto.Title = setting[0].Title;
            profileDto.ShortDescription = setting[0].ShortDescription;
            profileDto.ThumbnailUrl = setting[0].ThumbnailUrl;
            int pageSize = 4;
            int pageNumber = (request.Page ?? 1);
            profileDto.Posts = await _context.Posts!
                .Include(x => x.ApplicationUser)
                .Where(x => x.ApplicationUserId == user.Id)
                .OrderByDescending(x => x.CreatedDate)
                .ToPagedListAsync(pageNumber, pageSize);
            return new Result<ProfileDto>(ResultType.Ok).SetOutput(profileDto);
        }
    }
}
