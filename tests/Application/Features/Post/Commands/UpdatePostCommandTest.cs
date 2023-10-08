using BlogTemplate.Tests.Common;
using BlogTemplate.Application.Abstractions.Enums;
using Xunit;
using BlogTemplate.Application.Features.Post.Commands.Update;
using Microsoft.EntityFrameworkCore;
using BlogTemplate.Tests.Common.Extensions.DbContext.Post;

namespace BlogTemplate.Tests.Features.Post.Commands
{
    public class UpdatePostCommandTest : TestUserManagerWithDbContextCommandBase
    {
        public UpdatePostCommandTest()
            : base(DbContextBuilder.NewContext.AddPosts(), UserManagerFactory.Create())
        {
        }

        [Fact]
        public async Task UpdatePostCommandHandler_FailedWhenPostNotFound()
        {
            var idForUpdate = 0;
            var updateTitle = "New titile";
            var updatedDescription = "New description";
            var updatedShortDescription = "New short description";

            var handler = new UpdatePostCommandHandler(Context);

            var response = await handler.Handle(new UpdatePostCommand
            {
                Title = updateTitle, 
                Description = updatedDescription,
                ShortDescription = updatedShortDescription,
                ThumbnailUrl = null,
                PostId = idForUpdate
            }, CancellationToken.None);

            Assert.False(response.Conclusion);
            Assert.Equal(ErrorType.NotFound, response.ErrorDescription.ErrorType);
        }

        [Fact]
        public async Task UpdatePostCommandHandler_Success()
        {
            var idForUpdate = 1;
            var updateTitle = "New titile";
            var updatedDescription = "New description";
            var updatedShortDescription = "New short description";
            var updatedThumbnailUrl = "New thumbnailurl";
            var post = await Context.Posts.FirstOrDefaultAsync(x => x.Id == idForUpdate);
            string expectedThumbnail = new string(post.ThumbnailUrl);
            Assert.NotNull(post);

            var handler = new UpdatePostCommandHandler(Context);

            var response = await handler.Handle(new UpdatePostCommand
            {
                Title = updateTitle,
                Description = updatedDescription,
                ShortDescription = updatedShortDescription,
                ThumbnailUrl = updatedThumbnailUrl,
                PostId = idForUpdate
            }, CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.Equal(expectedThumbnail, response.Output.RemoveThumbnailUrl);

            Assert.Equal(post.ThumbnailUrl, updatedThumbnailUrl);
            Assert.Equal(post.ShortDescription, updatedShortDescription);
            Assert.Equal(post.Description, updatedDescription);
            Assert.Equal(post.Title, updateTitle);
        }
    }
}
