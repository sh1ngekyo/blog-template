using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Features.Comments.Commands.Update;
using BlogTemplate.Tests.Common;
using BlogTemplate.Tests.Common.Extensions.DbContext.Comments;
using Xunit;

namespace BlogTemplate.Tests.Features.Comments.Commands
{
    public class UpdateCommentCommandTest : TestDbContextCommandBase
    {

        public UpdateCommentCommandTest() : base(DbContextBuilder.NewContext.AddComments())
        {
        }

        [Fact]
        public async Task UpdateCommentCommandHandler_Success()
        {
            var handler = new UpdateCommentCommandHandler(Context);

            var comment = Context.Comments.Where(x => x.CommentId == 1).FirstOrDefault();
            var response = await handler.Handle(new UpdateCommentCommand
            {
                CommentId = 1,
                Content = "Updated"
            }, CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.Equal(ResultType.Updated, response.ResultType);
            var post = Context.Posts.FirstOrDefault(x => x.Id == 1);
            Assert.Equal(post.Slug, response.Output.PostSlug);
            Assert.Equal("Updated", comment.Content);
            Assert.NotEqual(comment.DateCreated, comment.DateModified);
        }
    }
}
