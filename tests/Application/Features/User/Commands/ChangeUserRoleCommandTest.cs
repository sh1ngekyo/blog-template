using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Tests.Common;

using Xunit;
using BlogTemplate.Application.Features.User.Commands.ChangeRole;

namespace BlogTemplate.Tests.Features.User.Commands
{
    public class ChangeUserRoleCommandTest : TestUserManagerCommandBase
    {
        public ChangeUserRoleCommandTest()
            : base(UserManagerFactory.Create())
        {
        }

        [Fact]
        public async Task ChangeUserRoleCommandHandler_SuccessOnAdmin()
        {
            var expectedMessage = $"User {UserManagerFactory.UserA} is author now";
            var handler = new ChangeUserRoleCommandHandler(UserManager);

            var response = await handler.Handle(new ChangeUserRoleCommand
            {
                Id = UserManagerFactory.UserAId,
                IsAdmin = true
            }, CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.Equal(expectedMessage, response.Output.ResultMessage);
        }

        [Fact]
        public async Task ChangeUserRoleCommandHandler_SuccessOnAuthor()
        {
            var expectedMessage = $"User {UserManagerFactory.UserB} is admin now";
            var handler = new ChangeUserRoleCommandHandler(UserManager);

            var response = await handler.Handle(new ChangeUserRoleCommand
            {
                Id = UserManagerFactory.UserBId,
                IsAdmin = false
            }, CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.Equal(expectedMessage, response.Output.ResultMessage);
        }

        [Fact]
        public async Task ChangeUserRoleCommandHandler_FailedWhenUserNotFound()
        {
            var expectedErrorType = ErrorType.NotFound;
            var handler = new ChangeUserRoleCommandHandler(UserManager);

            var response = await handler.Handle(new ChangeUserRoleCommand
            {
                Id = Guid.NewGuid().ToString(),
                IsAdmin = false
            }, CancellationToken.None);

            Assert.False(response.Conclusion);
            Assert.Equal(expectedErrorType, response.ErrorDescription.ErrorType);
        }
    }
}
