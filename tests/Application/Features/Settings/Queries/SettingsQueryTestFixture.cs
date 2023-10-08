using AutoMapper;

using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Common.Mappings;
using BlogTemplate.Infrastructure.Data;
using BlogTemplate.Tests.Common;
using BlogTemplate.Tests.Common.Extensions.DbContext.Settings;

using Xunit;

namespace BlogTemplate.Tests.Features.Settings.Queries
{
    public class SettingsQueryTestFixture : IDisposable
    {
        public ApplicationDbContext Context;
        public IMapper Mapper;

        public SettingsQueryTestFixture()
        {
            Context = DbContextBuilder.NewContext.AddSettings().Build();
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

    [CollectionDefinition("SettingsQueryCollection")]
    public class SettingsQueryCollection : ICollectionFixture<SettingsQueryTestFixture> { }
}
