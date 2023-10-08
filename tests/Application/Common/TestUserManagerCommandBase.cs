using BlogTemplate.Application.Abstractions;
using BlogTemplate.Domain.Models;

namespace BlogTemplate.Tests.Common
{
    public class TestUserManagerCommandBase
    {
        protected readonly IUserManagerProxy<ApplicationUser> UserManager;

        public TestUserManagerCommandBase(IUserManagerProxy<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }
    }
}
