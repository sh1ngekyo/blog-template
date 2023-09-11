using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Features.Profile.Commands.ResetPassword;
using BlogTemplate.Tests.Common;

using Xunit;

namespace BlogTemplate.Tests.Features.Profile.Commands
{
    public class ResetPasswordCommandTest : TestUserManagerCommandBase
    {
        public ResetPasswordCommandTest()
            : base(UserManagerFactory.Create())
        {
        }

        [Fact]
        public async Task ResetPasswordCommandHandler_Success()
        {
            var handler = new ResetPasswordCommandHandler(UserManager);

            var response = await handler.Handle(new ResetPasswordCommand
            {
                UserName = UserManagerFactory.UserA,
                NewPassword = Guid.NewGuid().ToString()
            }, CancellationToken.None);

            Assert.True(response.Conclusion);
        }

        [Fact]
        public async Task ResetPasswordCommandHandler_FailedWhenUserNotFound()
        {
            var handler = new ResetPasswordCommandHandler(UserManager);

            var response = await handler.Handle(new ResetPasswordCommand
            {
                UserName = Guid.NewGuid().ToString(),
                NewPassword = Guid.NewGuid().ToString()
            }, CancellationToken.None);

            Assert.False(response.Conclusion);
            Assert.Equal(ErrorType.NotFound, response.ErrorDescription.ErrorType);
        }
    }
}
