using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Domain.Models;
using MediatR;
using BlogTemplate.Domain;

namespace BlogTemplate.Application.Features.User.Commands.ChangeRole
{
    public class ChangeUserRoleCommandHandler 
        : IRequestHandler<ChangeUserRoleCommand, Result<ChangeUserRoleCommandResponse>>
    {
        private readonly IUserManagerProxy<ApplicationUser> _userManager;
        public ChangeUserRoleCommandHandler(IUserManagerProxy<ApplicationUser> userManager) =>
            _userManager = userManager;

        public async Task<Result<ChangeUserRoleCommandResponse>> Handle(ChangeUserRoleCommand request,
            CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByIdAsync(request.Id!);
            if (existingUser == null)
            {
                return new Result<ChangeUserRoleCommandResponse>(ErrorType.NotFound);
            }
            if (request.IsAdmin)
            {
                await _userManager.RemoveFromRoleAsync(existingUser, WebsiteRoles.WebsiteAdmin!);
                await _userManager.AddToRoleAsync(existingUser, WebsiteRoles.WebsiteAuthor!);
                return new Result<ChangeUserRoleCommandResponse>(ResultType.Ok)
                    .SetOutput(new ChangeUserRoleCommandResponse
                    {
                        ResultMessage = $"User {existingUser.UserName} is author now"
                    });
            }
            await _userManager.RemoveFromRoleAsync(existingUser, WebsiteRoles.WebsiteAuthor!);
            await _userManager.AddToRoleAsync(existingUser, WebsiteRoles.WebsiteAdmin!);
            return new Result<ChangeUserRoleCommandResponse>(ResultType.Ok)
                .SetOutput(new ChangeUserRoleCommandResponse
                {
                    ResultMessage = $"User {existingUser.UserName} is admin now"
                });
        }
    }
}
