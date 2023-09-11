using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Domain.Models;
using MediatR;

namespace BlogTemplate.Application.Features.Auth.Commands.SignOut
{
    public class SignOutCommandHandler : IRequestHandler<SignOutCommand, Result>
    {
        private readonly ISignInManagerProxy<ApplicationUser> _signInManager;
        public SignOutCommandHandler(ISignInManagerProxy<ApplicationUser> signInManager) =>
            _signInManager = signInManager;

        public async Task<Result> Handle(SignOutCommand request,
            CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
            return new Result(ResultType.Ok);
        }
    }
}
