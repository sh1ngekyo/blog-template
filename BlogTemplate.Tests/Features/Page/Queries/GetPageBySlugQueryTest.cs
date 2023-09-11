using AutoMapper;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Infrastructure.Data;
using Xunit;
using BlogTemplate.Application.Features.Page.Queries.GetBySlug;
using BlogTemplate.Tests.Common.Extensions.DbContext.Page;

namespace BlogTemplate.Tests.Features.Page.Queries
{
    [Collection("PageQueryCollection")]
    public class GetPageBySlugQueryTest
    {

        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;

        public GetPageBySlugQueryTest(PageQueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetPageBySlugQueryHandler_Success()
        {
            var handler = new GetPageBySlugQueryHandler(Context, Mapper);

            var response = await handler.Handle(
                new GetPageBySlugQuery()
                {
                    Slug = DbContextAddPageExtension.PageSlug
                },
                CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.NotNull(response.Output);
            Assert.NotNull(response.Output.Title);
            Assert.Equal(DbContextAddPageExtension.PageId, response.Output.Id);
        }

        [Fact]
        public async Task GetPageBySlugQueryHandler_FailOnWrongSlug()
        {
            var handler = new GetPageBySlugQueryHandler(Context, Mapper);

            var response = await handler.Handle(
                new GetPageBySlugQuery()
                {
                    Slug = Guid.NewGuid().ToString()
                },
                CancellationToken.None);

            Assert.False(response.Conclusion);
            Assert.Null(response.Output);
            Assert.Equal(ErrorType.NotFound, response.ErrorDescription.ErrorType);
        }
    }
}
