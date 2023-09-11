using BlogTemplate.Application.Features.Comments.Queries.GetAllByPostId;
using BlogTemplate.Infrastructure.Data;

using Xunit;

namespace BlogTemplate.Tests.Features.Comments.Queries
{
    [Collection("CommentQueryCollection")]
    public class GetAllCommentsByPostIdQueryTest
    {
        private readonly ApplicationDbContext Context;
        public GetAllCommentsByPostIdQueryTest(CommentQueryTestFixture fixture)
        {
            Context = fixture.Context;
        }

        [Fact]
        public async Task GetAllCommentsByPostIdQueryHandler_Success()
        {
            var handler = new GetAllCommentsByPostIdQueryHandler(Context);
            var response = await handler.Handle(
                new GetAllCommentsByPostIdQuery()
                {
                    PostId = 1
                },
                CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.NotNull(response.Output);
            Assert.Equal(
                Context.Comments.Where(x => x.PostId == 1 && x.ParentId == null).Count(),
                response.Output.Count);
            foreach(var comment in response.Output)
            {
                Assert.Equal(
                    Context.Comments.Where(x => x.ParentId == comment.CommentId).Count(),
                    comment.Children.Count);
            }
        }
    }
}
