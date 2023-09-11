using AutoMapper;
using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Features.Settings.Commands.Update;
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BlogTemplate.Domain.Models;

namespace BlogTemplate.Application.Features.User.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<DeleteUserCommandResponse>>
    {
        private readonly IUserManagerProxy<ApplicationUser> _userManager;
        public DeleteUserCommandHandler(IUserManagerProxy<ApplicationUser> userManager) =>
            _userManager = userManager;

        public async Task<Result<DeleteUserCommandResponse>> Handle(DeleteUserCommand request,
            CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByIdAsync(request.Id!);
            if (existingUser == null)
            {
                return new Result<DeleteUserCommandResponse>(ErrorType.NotFound);
            }
            await _userManager.DeleteAsync(existingUser);
            return new Result<DeleteUserCommandResponse>(ResultType.Deleted).SetOutput(
               new DeleteUserCommandResponse 
               {
                   RemoveThumbnailUrl = existingUser!.ThumbnailUrl,
                   DeletedUserName = existingUser.UserName 
               });
        }
    }
}
