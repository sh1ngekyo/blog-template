using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Tests.Common;

using Xunit;
using BlogTemplate.Application.Features.User.Commands.Delete;

namespace BlogTemplate.Tests.Features.User.Commands
{
    public class DeleteUserCommandTest : TestUserManagerCommandBase
    {
        public DeleteUserCommandTest()
            : base(UserManagerFactory.Create())
        {
        }

        [Fact]
        public async Task DeleteUserCommandHandler_Success()
        {
            var expectedUserName = UserManagerFactory.UserA;
            var handler = new DeleteUserCommandHandler(UserManager);

            var response = await handler.Handle(new DeleteUserCommand
            {
                Id = UserManagerFactory.UserAId
            }, CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.Equal(ResultType.Deleted, response.ResultType);
            Assert.NotNull(response.Output.RemoveThumbnailUrl);
            Assert.NotNull(response.Output.DeletedUserName);
            Assert.Equal(expectedUserName, response.Output.DeletedUserName);
        }

        [Fact]
        public async Task DeleteUserCommandHandler_FailedWhenUserNotFound()
        {
            var expectedErrorType = ErrorType.NotFound;
            var handler = new DeleteUserCommandHandler(UserManager);

            var response = await handler.Handle(new DeleteUserCommand
            {
                Id = Guid.NewGuid().ToString()
            }, CancellationToken.None);

            Assert.False(response.Conclusion);
            Assert.Null(response.Output);
            Assert.Equal(expectedErrorType, response.ErrorDescription.ErrorType);
        }
    }
}
