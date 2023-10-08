using BlogTemplate.Infrastructure.Data;
using BlogTemplate.Tests.Common;

using Xunit;
using BlogTemplate.Tests.Common.Extensions.DbContext.Comments;

namespace BlogTemplate.Tests.Features.Comments.Queries
{
    public class CommentQueryTestFixture
    {
        public ApplicationDbContext Context;

        public CommentQueryTestFixture()
        {
            Context = DbContextBuilder.NewContext.AddComments().Build();
        }
    }

    [CollectionDefinition("CommentQueryCollection")]
    public class CommentQueryCollection : ICollectionFixture<CommentQueryTestFixture> { }
}
