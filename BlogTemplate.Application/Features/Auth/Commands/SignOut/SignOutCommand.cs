using BlogTemplate.Application.Abstractions;
using MediatR;

namespace BlogTemplate.Application.Features.Auth.Commands.SignOut
{
    public class SignOutCommand : IRequest<Result>
    {
    }
}
