using AutoMapper;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Features.Post.Queries.GetAllForCurrentUser;
using BlogTemplate.Domain.Models;
using BlogTemplate.Infrastructure.Data;
using BlogTemplate.Tests.Common;

using Xunit;

namespace BlogTemplate.Tests.Features.Post.Queries
{
    [Collection("PostQueryCollection")]
    public class GetAllPostsForCurrentUserQueryTest
    {
        private readonly IMapper Mapper;
        private readonly ApplicationDbContext Context;
        private readonly IUserManagerProxy<ApplicationUser> UserManager;
        public GetAllPostsForCurrentUserQueryTest(PostQueryTestFixture fixture)
        {
            UserManager = fixture.UserManager;
            Mapper = fixture.Mapper;
            Context = fixture.Context;
        }

        [Fact]
        public async Task GetAllPostsForCurrentUserQueryHandler_SuccessReturnAllForAdmin()
        {
            var handler = new GetAllPostsForCurrentUserQueryHandler(UserManager, Context, Mapper);

            var response = await handler.Handle(
                new GetAllPostsForCurrentUserQuery()
                {
                    UserName = UserManagerFactory.UserB
                },
                CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.NotNull(response.Output);
            Assert.Equal(Context.Posts!.Count(), response.Output.Count);
        }

        [Fact]
        public async Task GetAllPostsForCurrentUserQueryHandler_SuccessReturnOnlyOwnedForAuthor()
        {
            var handler = new GetAllPostsForCurrentUserQueryHandler(UserManager, Context, Mapper);

            var response = await handler.Handle(
                new GetAllPostsForCurrentUserQuery()
                {
                    UserName = UserManagerFactory.UserA
                },
                CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.NotNull(response.Output);
            Assert.Equal(
                Context.Posts!.Where(x => x.ApplicationUserId == UserManagerFactory.UserAId).Count(),
                response.Output.Count);
        }
    }
}
