using BlogTemplate.Infrastructure.Data;
using BlogTemplate.Tests.Common;
using BlogTemplate.Tests.Common.Extensions.DbContext.Home;
using BlogTemplate.Tests.Common.Extensions.DbContext.Settings;

using Xunit;

namespace BlogTemplate.Tests.Features.Home.Queries
{
    public class HomeQueryTestFixture : IDisposable
    {
        public ApplicationDbContext Context;

        public HomeQueryTestFixture()
        {
            Context = DbContextBuilder.NewContext.AddHome().AddSettings().Build();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }

    [CollectionDefinition("HomeQueryCollection")]
    public class HomeQueryCollection : ICollectionFixture<HomeQueryTestFixture> { }
}
