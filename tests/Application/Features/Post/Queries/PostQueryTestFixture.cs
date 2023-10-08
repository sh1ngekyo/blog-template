using AutoMapper;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Common.Mappings;
using BlogTemplate.Domain.Models;
using BlogTemplate.Infrastructure.Data;
using BlogTemplate.Tests.Common;
using BlogTemplate.Tests.Common.Extensions.DbContext.Post;

using Xunit;

namespace BlogTemplate.Tests.Features.Post.Queries
{
    public class PostQueryTestFixture
    {
        public IMapper Mapper;
        public ApplicationDbContext Context;
        public IUserManagerProxy<ApplicationUser> UserManager;

        public PostQueryTestFixture()
        {
            Context = DbContextBuilder.NewContext.AddPosts().Build();
            UserManager = UserManagerFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(IApplicationDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }
    }

    [CollectionDefinition("PostQueryCollection")]
    public class PostQueryCollection : ICollectionFixture<PostQueryTestFixture> { }
}
