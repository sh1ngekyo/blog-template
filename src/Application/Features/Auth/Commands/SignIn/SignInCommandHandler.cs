using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogTemplate.Application.Features.Auth.Commands.SignIn
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, Result>
    {
        private readonly IUserManagerProxy<ApplicationUser> _userManager;
        private readonly ISignInManagerProxy<ApplicationUser> _signInManager;
        public SignInCommandHandler(IUserManagerProxy<ApplicationUser> userManager, ISignInManagerProxy<ApplicationUser> signInManager) =>
            (_userManager, _signInManager) = (userManager, signInManager);

        public async Task<Result> Handle(SignInCommand request,
            CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == request.Username);
            if (existingUser == null)
            {
                return new Result(ErrorType.NotValid, "Username does not exist");
            }
            var verifyPassword = await _userManager.CheckPasswordAsync(existingUser, request.Password!);
            if (!verifyPassword)
            {
                return new Result(ErrorType.NotValid, "Password does not match");
            }

            await _signInManager.PasswordSignInAsync(request.Username, request.Password, request.RememberMe, request.IsLockout);
            return new Result(ResultType.Ok);
        }
    }
}
