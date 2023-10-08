using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Tests.Common;
using Xunit;
using BlogTemplate.Application.Features.Post.Commands.Create;

namespace BlogTemplate.Tests.Features.Post.Commands
{
    public class CreatePostCommandTest : TestUserManagerWithDbContextCommandBase
    {
        public CreatePostCommandTest()
            : base(DbContextBuilder.NewContext, UserManagerFactory.Create())
        {
        }

        [Fact]
        public async Task CreatePostCommandHandler_Success()
        {
            var expectedUserName = UserManagerFactory.UserA;
            var expectedTitle = "Test title";
            var expectedDescription = "Test description";
            var expectedShortDescription = "Test short description";

            var handler = new CreatePostCommandHandler(UserManager, Context);

            var response = await handler.Handle(new CreatePostCommand
            {
                UserName = expectedUserName,
                Title = expectedTitle,
                Description = expectedDescription,
                ShortDescription = expectedShortDescription,
                ThumbnailUrl = null,
            }, CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.Equal(ResultType.Created, response.ResultType);

            var post = Context.Posts.FirstOrDefault(x => x.ApplicationUserId == UserManagerFactory.UserAId);

            Assert.NotNull(post);
            Assert.Equal(1, post.Id);
            Assert.Equal(UserManagerFactory.UserAId, post.ApplicationUserId);
            Assert.Equal(expectedTitle, post.Title);
            Assert.Equal(expectedDescription, post.Description);
            Assert.Equal(expectedShortDescription, post.ShortDescription);
            Assert.NotNull(post.Slug);
            Assert.Null(post.ThumbnailUrl);
        }
    }
}
