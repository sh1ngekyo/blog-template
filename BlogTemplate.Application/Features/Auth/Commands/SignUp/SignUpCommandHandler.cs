using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using MediatR;
using BlogTemplate.Domain.Models;
using BlogTemplate.Domain;

namespace BlogTemplate.Application.Features.Auth.Commands.SignUp
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Result>
    {
        private readonly IUserManagerProxy<ApplicationUser> _userManager;
        private readonly ISignInManagerProxy<ApplicationUser> _signInManager;
        public SignUpCommandHandler(IUserManagerProxy<ApplicationUser> userManager, ISignInManagerProxy<ApplicationUser> signInManager) =>
            (_userManager, _signInManager) = (userManager, signInManager);

        public async Task<Result> Handle(SignUpCommand request,
            CancellationToken cancellationToken)
        {
            var checkUserByEmail = await _userManager.FindByEmailAsync(request.Email!);
            if (checkUserByEmail != null)
            {
                return new Result(ErrorType.NotValid, "Email already exists");
            }
            var checkUserByUsername = await _userManager.FindByUserNameAsync(request.UserName!);
            if (checkUserByUsername != null)
            {
                return new Result(ErrorType.NotValid, "Username already exists");
            }

            var applicationUser = new ApplicationUser()
            {
                Email = request.Email,
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var created = await _userManager.CreateAsync(applicationUser, request.Password!);
            if (created)
            {
                await _userManager.AddToRoleAsync(applicationUser, WebsiteRoles.WebsiteAuthor!);
                await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, true);
                return new Result(ResultType.Ok);
            }
            return new Result(ErrorType.Unknown);
        }
    }
}
