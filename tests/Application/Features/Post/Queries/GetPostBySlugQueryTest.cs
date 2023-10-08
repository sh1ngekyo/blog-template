using AutoMapper;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Features.Post.Queries.GetBySlug;
using BlogTemplate.Domain.Models;
using BlogTemplate.Infrastructure.Data;

using Xunit;

namespace BlogTemplate.Tests.Features.Post.Queries
{
    [Collection("PostQueryCollection")]
    public class GetPostBySlugQueryTest
    {
        private readonly IMapper Mapper;
        private readonly ApplicationDbContext Context;
        private readonly IUserManagerProxy<ApplicationUser> UserManager;
        public GetPostBySlugQueryTest(PostQueryTestFixture fixture)
        {
            UserManager = fixture.UserManager;
            Mapper = fixture.Mapper;
            Context = fixture.Context;
        }

        [Fact]
        public async Task GetPostBySlugQueryHandler_Success()
        {
            var handler = new GetPostBySlugQueryHandler(Context, Mapper);
            var postId = 1;
            var expectedSlug = Context.Posts.FirstOrDefault(x => x.Id == 1).Slug;

            var response = await handler.Handle(
                new GetPostBySlugQuery()
                {
                    Slug = expectedSlug
                },
                CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.NotNull(response.Output);
            Assert.Equal(expectedSlug, response.Output.Slug);
        }

        [Fact]
        public async Task GetPostBySlugQueryHandler_FailedWhenPostNotFound()
        {
            var handler = new GetPostBySlugQueryHandler(Context, Mapper);
            var expectedSlug = Guid.NewGuid().ToString();

            var response = await handler.Handle(
                new GetPostBySlugQuery()
                {
                    Slug = expectedSlug
                },
                CancellationToken.None);

            Assert.False(response.Conclusion);
            Assert.Null(response.Output);
            Assert.Equal(ErrorType.NotFound, response.ErrorDescription.ErrorType);
        }
    }
}
