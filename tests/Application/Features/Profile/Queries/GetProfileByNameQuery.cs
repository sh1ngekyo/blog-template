using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Features.Profile.Queries.GetByName;
using BlogTemplate.Domain.Models;
using BlogTemplate.Infrastructure.Data;
using BlogTemplate.Tests.Common;

using Xunit;

namespace BlogTemplate.Tests.Features.Profile.Queries
{
    [Collection("ProfileQueryCollection")]
    public class GetProfileByNameQuery
    {
        private readonly ApplicationDbContext Context;
        private readonly IUserManagerProxy<ApplicationUser> UserManager;
        public GetProfileByNameQuery(ProfileQueryTestFixture fixture)
        {
            UserManager = fixture.UserManager;
            Context = fixture.Context;
        }

        [Fact]
        public async Task GetProfileByNameQueryHandler_Success()
        {
            var handler = new GetProfileByUserNameQueryHandler(Context, UserManager);

            var response = await handler.Handle(
                new GetProfileByUserNameQuery()
                {
                    UserName = UserManagerFactory.UserA,
                    Page = 1,
                },
                CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.NotNull(response.Output);
            var posts = Context.Posts.Where(x => x.ApplicationUserId == UserManagerFactory.UserAId)
                .OrderByDescending(x => x.CreatedDate).ToList();
            Assert.Equal(posts.Count, response.Output.Posts.Count);
            for (var i = 0; i < posts.Count(); i++) 
            {
                Assert.Equal(posts[i], response.Output.Posts[i]);
            }
        }

        [Fact]
        public async Task GetProfileByNameQueryHandler_FailedWhenUserNotFound()
        {
            var handler = new GetProfileByUserNameQueryHandler(Context, UserManager);

            var response = await handler.Handle(
                new GetProfileByUserNameQuery()
                {
                    UserName = Guid.NewGuid().ToString(),
                    Page = 1,
                },
                CancellationToken.None);

            Assert.False(response.Conclusion);
            Assert.Null(response.Output);
            Assert.Equal(ErrorType.NotFound, response.ErrorDescription.ErrorType);
        }
    }
}
