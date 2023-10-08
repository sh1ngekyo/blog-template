using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using MediatR;
using BlogTemplate.Domain.Models;

namespace BlogTemplate.Application.Features.Profile.Commands.Update
{
    public class UpdateProfileCommandHandler 
        : IRequestHandler<UpdateProfileCommand, Result<UpdateProfileCommandResponse>>
    {
        private readonly IUserManagerProxy<ApplicationUser> _userManager;
        public UpdateProfileCommandHandler(IUserManagerProxy<ApplicationUser> userManager) =>
            _userManager = userManager;

        public async Task<Result<UpdateProfileCommandResponse>> Handle(UpdateProfileCommand request,
            CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByUserNameAsync(request.UserName!);
            if (existingUser == null)
            {
                return new Result<UpdateProfileCommandResponse>(ErrorType.NotFound);
            }
            var response = new UpdateProfileCommandResponse();
            existingUser.LastName = request.LastName;
            existingUser.FirstName = request.FirstName;
            existingUser.Email = request.Email;
            existingUser.About = request.About;
            if (existingUser.ThumbnailUrl != request.ThumbnailUrl)
            {
                response.RemoveThumbnailUrl = existingUser.ThumbnailUrl;
            }
            existingUser.ThumbnailUrl = request.ThumbnailUrl;
            await _userManager.UpdateAsync(existingUser);
            return new Result<UpdateProfileCommandResponse>(ResultType.Updated).SetOutput(response);
        }
    }
}
