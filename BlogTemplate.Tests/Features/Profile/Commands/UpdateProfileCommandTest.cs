using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Features.Profile.Commands.ResetPassword;
using BlogTemplate.Tests.Common;
using Xunit;
using BlogTemplate.Application.Features.Profile.Commands.Update;

namespace BlogTemplate.Tests.Features.Profile.Commands
{
    public class UpdateProfileCommandTest : TestUserManagerCommandBase
    {
        public UpdateProfileCommandTest()
            : base(UserManagerFactory.Create())
        {
        }

        [Fact]
        public async Task UpdateProfileCommandHandler_Success()
        {
            var handler = new UpdateProfileCommandHandler(UserManager);

            var response = await handler.Handle(new UpdateProfileCommand
            {
                UserName = UserManagerFactory.UserA,
                ThumbnailUrl = "New"
            }, CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.NotNull(response.Output.RemoveThumbnailUrl);
            Assert.Equal("ThumbnailUrl", response.Output.RemoveThumbnailUrl);
        }

        [Fact]
        public async Task UpdateProfileCommandHandler_FailedWhenUserNotFound()
        {
            var handler = new UpdateProfileCommandHandler(UserManager);

            var response = await handler.Handle(new UpdateProfileCommand
            {
                UserName = Guid.NewGuid().ToString(),
            }, CancellationToken.None);

            Assert.False(response.Conclusion);
            Assert.Equal(ErrorType.NotFound, response.ErrorDescription.ErrorType);
        }
    }
}
