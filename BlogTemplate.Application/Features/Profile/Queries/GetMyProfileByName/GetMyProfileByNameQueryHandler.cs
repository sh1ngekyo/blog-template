using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Domain.Models;
using MediatR;
using BlogTemplate.Application.DataTransfer.User;

namespace BlogTemplate.Application.Features.Profile.Queries.GetMyProfileByName
{
    public class GetMyProfileByNameQueryHandler 
        : IRequestHandler<GetMyProfileByNameQuery, Result<UserDto>>
    {
        private readonly IUserManagerProxy<ApplicationUser> _userManager;
        public GetMyProfileByNameQueryHandler(IUserManagerProxy<ApplicationUser> userManager) =>
            _userManager = userManager;

        public async Task<Result<UserDto>> Handle(GetMyProfileByNameQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByUserNameAsync(request.UserName!);
            if (user == null)
            {
                return new Result<UserDto>(ErrorType.NotFound);
            }
            var dto = new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                About = user.About,
                ThumbnailUrl = user.ThumbnailUrl,
            };
            var role = await _userManager.GetRolesAsync(user);
            dto.Role = role.FirstOrDefault();
            return new Result<UserDto>(ResultType.Ok).SetOutput(dto);
        }
    }
}
