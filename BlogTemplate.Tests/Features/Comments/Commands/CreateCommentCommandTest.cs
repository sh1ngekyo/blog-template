using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Features.Comments.Commands.Create;
using BlogTemplate.Tests.Common;
using BlogTemplate.Tests.Common.Extensions.DbContext.Comments;

using Xunit;

namespace BlogTemplate.Tests.Features.Comments.Commands
{
    public class CreateCommentCommandTest : TestDbContextCommandBase
    {
        public CreateCommentCommandTest() : base(DbContextBuilder.NewContext.AddComments())
        {
        }

        [Fact]
        public async Task CreateCommentCommandHandler_Success()
        {
            var handler = new CreateCommentCommandHandler(Context);

            var response = await handler.Handle(new CreateCommentCommand
            {
                ApplicationUserId = "TestUserD",
                Content = "Test",
                ParentId = null,
                PostId = 1
            }, CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.Equal(ResultType.Created, response.ResultType);

            var post = Context.Posts.FirstOrDefault(x => x.Id == 1);
            var comment = Context.Comments.Where(x => x.ApplicationUserId == "TestUserD").FirstOrDefault();
            Assert.Equal(post.Slug, response.Output.PostSlug);
            Assert.Equal("Test", comment.Content);
        }
    }
}
