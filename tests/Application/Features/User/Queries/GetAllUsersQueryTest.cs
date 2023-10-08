using AutoMapper;
using BlogTemplate.Application.Features.User.Queries.GetAll;
using Xunit;
using BlogTemplate.Domain.Models;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Tests.Common;

namespace BlogTemplate.Tests.Features.User.Queries
{
    [Collection("UserQueryCollection")]
    public class GetAllUsersQueryTest
    {
        private readonly IMapper Mapper;
        private readonly IUserManagerProxy<ApplicationUser> UserManager;
        public GetAllUsersQueryTest(UserQueryTestFixture fixture)
        {
            UserManager = fixture.UserManager;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetAllUsersQueryHandler_Success()
        {
            var handler = new GetAllUsersQueryHandler(UserManager, Mapper);

            var response = await handler.Handle(
                new GetAllUsersQuery(),
                CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.NotNull(response.Output);
            Assert.NotEmpty(response.Output);
            Assert.Equal(UserManagerFactory.UserA, response.Output[0].UserName);
            Assert.Equal(UserManagerFactory.UserARole, response.Output[0].Role);
            Assert.Equal(UserManagerFactory.UserB, response.Output[1].UserName);
            Assert.Equal(UserManagerFactory.UserBRole, response.Output[1].Role);
        }
    }
}
