using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Features.Post.Commands.Delete;
using BlogTemplate.Domain.Models;
using BlogTemplate.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BlogTemplate.Application.Features.Profile.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
    {
        private readonly IUserManagerProxy<ApplicationUser> _userManager;
        public ResetPasswordCommandHandler(IUserManagerProxy<ApplicationUser> userManager) =>
            _userManager = userManager;
        public async Task<Result> Handle(ResetPasswordCommand request,
            CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByUserNameAsync(request.UserName!);
            if (existingUser == null)
            {
                return new Result(ErrorType.NotFound, "User doesnot exist");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
            await _userManager.ResetPasswordAsync(existingUser, token, request.NewPassword!);
            return new Result(ResultType.Ok);
        }
    }
}
