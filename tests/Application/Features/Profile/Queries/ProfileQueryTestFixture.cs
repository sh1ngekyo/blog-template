using BlogTemplate.Application.Abstractions;
using BlogTemplate.Domain.Models;
using BlogTemplate.Infrastructure.Data;
using BlogTemplate.Tests.Common;

using Xunit;
using BlogTemplate.Tests.Common.Extensions.DbContext.Post;
using BlogTemplate.Tests.Common.Extensions.DbContext.Settings;

namespace BlogTemplate.Tests.Features.Profile.Queries
{
    public class ProfileQueryTestFixture
    {
        public ApplicationDbContext Context;
        public IUserManagerProxy<ApplicationUser> UserManager;

        public ProfileQueryTestFixture()
        {
            Context = DbContextBuilder.NewContext.AddPosts().AddSettings().Build();
            UserManager = UserManagerFactory.Create();
        }
    }

    [CollectionDefinition("ProfileQueryCollection")]
    public class ProfileQueryCollection : ICollectionFixture<ProfileQueryTestFixture> { }
}
