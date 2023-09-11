using AutoMapper;

using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Common.Mappings;
using BlogTemplate.Infrastructure.Data;
using BlogTemplate.Tests.Common;
using BlogTemplate.Tests.Common.Extensions.DbContext.Page;
using BlogTemplate.Tests.Common.Extensions.DbContext.Settings;

using Xunit;

namespace BlogTemplate.Tests.Features.Page.Queries
{
    public class PageQueryTestFixture : IDisposable
    {
        public ApplicationDbContext Context;
        public IMapper Mapper;

        public PageQueryTestFixture()
        {
            Context = DbContextBuilder.NewContext.AddPage().AddSettings().Build();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(IApplicationDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }

    [CollectionDefinition("PageQueryCollection")]
    public class PageQueryCollection : ICollectionFixture<PageQueryTestFixture> { }
}
