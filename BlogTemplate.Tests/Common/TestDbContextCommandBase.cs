using BlogTemplate.Infrastructure.Data;

namespace BlogTemplate.Tests.Common
{
    public abstract class TestDbContextCommandBase : IDisposable
    {
        protected readonly ApplicationDbContext Context;

        public TestDbContextCommandBase(ApplicationDbContext context)
        {
            Context = context.Build();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
