using BlogTemplate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogTemplate.Tests.Common
{
    public static class DbContextBuilder
    {
        public static ApplicationDbContext NewContext
            => new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        public static ApplicationDbContext Build(this ApplicationDbContext context)
        {
            context.ChangeTracker.Clear();
            context.Database.EnsureCreated();
            return context;
        }
    }
}
