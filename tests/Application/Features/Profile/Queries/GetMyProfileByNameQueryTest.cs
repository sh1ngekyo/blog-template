using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Domain.Models;
using BlogTemplate.Infrastructure.Data;

using Xunit;
using BlogTemplate.Application.Features.Profile.Queries.GetMyProfileByName;
using BlogTemplate.Tests.Common;

namespace BlogTemplate.Tests.Features.Profile.Queries
{
    [Collection("ProfileQueryCollection")]
    public class GetMyProfileByNameQueryTest
    {
        private readonly ApplicationDbContext Context;
        private readonly IUserManagerProxy<ApplicationUser> UserManager;
        public GetMyProfileByNameQueryTest(ProfileQueryTestFixture fixture)
        {
            UserManager = fixture.UserManager;
            Context = fixture.Context;
        }

        [Fact]
        public async Task GetMyProfileByNameQueryHandler_Success()
        {
            var handler = new GetMyProfileByNameQueryHandler(UserManager);

            var response = await handler.Handle(
                new GetMyProfileByNameQuery()
                {
                    UserName = UserManagerFactory.UserA
                },
                CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.NotNull(response.Output);
            Assert.Equal(UserManagerFactory.UserA, response.Output.UserName);
            Assert.Equal(UserManagerFactory.UserARole, response.Output.Role);
            Assert.Equal(UserManagerFactory.UserAId, response.Output.Id);
        }

        [Fact]
        public async Task GetMyProfileByNameQueryHandler_FailedWhenUserNotFound()
        {
            var handler = new GetMyProfileByNameQueryHandler(UserManager);

            var response = await handler.Handle(
                new GetMyProfileByNameQuery()
                {
                    UserName = Guid.NewGuid().ToString()
                },
                CancellationToken.None);

            Assert.False(response.Conclusion);
            Assert.Null(response.Output);
            Assert.Equal(ErrorType.NotFound, response.ErrorDescription.ErrorType);
        }
    }
}
