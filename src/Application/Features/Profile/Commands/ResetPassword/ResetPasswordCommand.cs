using BlogTemplate.Application.Abstractions;

using MediatR;

namespace BlogTemplate.Application.Features.Profile.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<Result>
    {
        public string? UserName { get; set; }
        public string? NewPassword { get; set; }
    }
}
