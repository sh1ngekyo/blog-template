using AutoMapper;

using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Common.Mappings;
using BlogTemplate.Domain.Models;
using BlogTemplate.Tests.Common;

using Xunit;

namespace BlogTemplate.Tests.Features.User.Queries
{
    public class UserQueryTestFixture
    {
        public IMapper Mapper;
        public IUserManagerProxy<ApplicationUser> UserManager;

        public UserQueryTestFixture()
        {
            UserManager = UserManagerFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(IApplicationDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }
    }

    [CollectionDefinition("UserQueryCollection")]
    public class UserQueryCollection : ICollectionFixture<UserQueryTestFixture> { }
}
