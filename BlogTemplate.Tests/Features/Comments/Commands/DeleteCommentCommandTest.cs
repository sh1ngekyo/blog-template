using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Features.Comments.Commands.Delete;
using BlogTemplate.Tests.Common;
using BlogTemplate.Tests.Common.Extensions.DbContext.Comments;

using Xunit;

namespace BlogTemplate.Tests.Features.Comments.Commands
{
    public class DeleteCommentCommandTest : TestDbContextCommandBase
    {

        public DeleteCommentCommandTest() : base(DbContextBuilder.NewContext.AddComments())
        {
        }

        [Fact]
        public async Task DeleteCommentCommandHandler_Success()
        {
            var handler = new DeleteCommentCommandHandler(Context);

            var comment = Context.Comments.Where(x => x.CommentId == 1).FirstOrDefault();
            Assert.NotEmpty(Context.Comments.Where(x => x.ParentId == 1));

            var response = await handler.Handle(new DeleteCommentCommand
            {
                CommentId = 1
            }, CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.Equal(ResultType.Deleted, response.ResultType);
            var post = Context.Posts.FirstOrDefault(x => x.Id == 1);
            Assert.Equal(post.Slug, response.Output.PostSlug);
            Assert.Empty(Context.Comments.Where(x => x.CommentId == comment.CommentId).ToList());
            Assert.Empty(Context.Comments.Where(x => x.ParentId == comment.CommentId).ToList());
        }
    }
}
