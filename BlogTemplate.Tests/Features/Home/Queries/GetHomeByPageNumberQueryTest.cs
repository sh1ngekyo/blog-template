using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Common.Behaviors;
using BlogTemplate.Application.Features.Home.Queries.GetByPageNumber;
using BlogTemplate.Infrastructure.Data;

using Xunit;
using BlogTemplate.Application.DataTransfer.Home;

namespace BlogTemplate.Tests.Features.Home.Queries
{
    [Collection("HomeQueryCollection")]
    public class GetHomeByPageNumberQueryTest 
    {
        private readonly ApplicationDbContext Context;

        public GetHomeByPageNumberQueryTest(HomeQueryTestFixture fixture)
        {
            Context = fixture.Context;
        }

        [Fact]
        public async Task GetHomeByPageNumberQueryHandler_Success()
        {
            var pageSize = 4;
            var pageNumber = 1;
            var handler = new GetHomeByPageNumberQueryHandler(Context);

            var response = await handler.Handle(
                new GetHomeByPageNumberQuery()
                {
                    Page = pageNumber,
                    PageSize = pageSize
                },
                CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.NotNull(response.Output);
            Assert.NotNull(response.Output.Posts);
            Assert.NotEmpty(response.Output.Posts);
            Assert.Equal(pageSize, response.Output.Posts.Count);
            Assert.Equal(pageSize, response.Output.Posts.Count);

            foreach (var post in response.Output.Posts)
                Assert.NotNull(post.ApplicationUserId);

            response = await handler.Handle(
                new GetHomeByPageNumberQuery()
                {
                    Page = 2,
                    PageSize = pageSize
                },
                CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.NotNull(response.Output);
            Assert.NotNull(response.Output.Posts);
            Assert.NotEmpty(response.Output.Posts);
            Assert.Equal(1, response.Output.Posts.Count);
        }

        [Fact]
        public async Task GetHomeByPageNumberQueryValidator_FailedOnWrongPageNumber()
        {
            var pageSize = 4;
            var pageNumber = 0;

            var getHomeByPageNumberQuery = new GetHomeByPageNumberQuery()
            {
                Page = pageNumber,
                PageSize = pageSize
            };
            var getHomeByPageNumberQueryHandler = new GetHomeByPageNumberQueryHandler(Context);

            var validationBehavior = new ValidationBehavior<GetHomeByPageNumberQuery, Result<HomeDto>>
            (new List<GetHomeByPageNumberQueryValidator>()
            {
                new GetHomeByPageNumberQueryValidator()
            });

            var response = await validationBehavior.Handle(
                getHomeByPageNumberQuery,
                async ()
                    => await getHomeByPageNumberQueryHandler.Handle(getHomeByPageNumberQuery,
                    CancellationToken.None),
                CancellationToken.None);

            Assert.False(response.Conclusion);
            Assert.Equal(ErrorType.NotValid, response.ErrorDescription.ErrorType);
        }
    }
}
