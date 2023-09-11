using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Application.Features.User.Commands.Delete
{
    public class DeleteUserCommandResponse
    {
        public string? RemoveThumbnailUrl { get; set; }
        public string? DeletedUserName { get; set; }
    }
}
