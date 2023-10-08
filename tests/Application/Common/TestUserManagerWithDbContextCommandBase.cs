using BlogTemplate.Application.Abstractions;
using BlogTemplate.Domain.Models;
using BlogTemplate.Infrastructure.Data;

namespace BlogTemplate.Tests.Common
{
    public class TestUserManagerWithDbContextCommandBase : IDisposable
    {
        protected readonly ApplicationDbContext Context;
        protected readonly IUserManagerProxy<ApplicationUser> UserManager;

        public TestUserManagerWithDbContextCommandBase(ApplicationDbContext context, IUserManagerProxy<ApplicationUser> userManagerProxy)
        {
            Context = context.Build();
            UserManager = userManagerProxy;
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
