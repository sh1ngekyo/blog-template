using BlogTemplate.Application.Abstractions;
using MediatR;

namespace BlogTemplate.Application.Features.Auth.Commands.SignIn
{
    public class SignInCommand : IRequest<Result>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
        public bool IsLockout { get; set; } = true;
    }
}
