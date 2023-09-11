using AutoMapper;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Features.User.Queries.GetByName;
using BlogTemplate.Domain.Models;
using BlogTemplate.Tests.Common;

using Xunit;

namespace BlogTemplate.Tests.Features.User.Queries
{
    [Collection("UserQueryCollection")]
    public class GetUserByNameQueryTest
    {
        private readonly IMapper Mapper;
        private readonly IUserManagerProxy<ApplicationUser> UserManager;
        public GetUserByNameQueryTest(UserQueryTestFixture fixture)
        {
            UserManager = fixture.UserManager;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetUserByNameQueryHandler_Success()
        {
            var handler = new GetUserByNameQueryHandler(UserManager, Mapper);

            var response = await handler.Handle(
                new GetUserByNameQuery()
                {
                    UserName = UserManagerFactory.UserA
                },
                CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.NotNull(response.Output);
            Assert.Equal(UserManagerFactory.UserA, response.Output.UserName);
            Assert.Equal(UserManagerFactory.UserARole, response.Output.Role);
        }

        [Fact]
        public async Task GetUserByNameQueryHandler_FailedWhenNotFound()
        {
            var handler = new GetUserByNameQueryHandler(UserManager, Mapper);

            var response = await handler.Handle(
                new GetUserByNameQuery()
                {
                    UserName = Guid.NewGuid().ToString(),
                },
                CancellationToken.None);

            Assert.False(response.Conclusion);
            Assert.Null(response.Output);
            Assert.Equal(ErrorType.NotFound, response.ErrorDescription.ErrorType);
        }
    }
}
