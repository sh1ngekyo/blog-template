using BlogTemplate.Application.Abstractions;

using MediatR;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Application.Features.Profile.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<Result>
    {
        public string? UserName { get; set; }
        public string? NewPassword { get; set; }
    }
}
