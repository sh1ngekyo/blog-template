using AutoMapper;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Features.Post.Queries.GetById;
using BlogTemplate.Domain.Models;
using BlogTemplate.Infrastructure.Data;

using Xunit;

namespace BlogTemplate.Tests.Features.Post.Queries
{
    [Collection("PostQueryCollection")]
    public class GetPostByIdQueryTest
    {
        private readonly IMapper Mapper;
        private readonly ApplicationDbContext Context;
        private readonly IUserManagerProxy<ApplicationUser> UserManager;
        public GetPostByIdQueryTest(PostQueryTestFixture fixture)
        {
            UserManager = fixture.UserManager;
            Mapper = fixture.Mapper;
            Context = fixture.Context;
        }

        [Fact]
        public async Task GetPostByIdQueryHandler_Success()
        {
            var handler = new GetPostByIdQueryHandler(Context, Mapper);
            var postId = 1;
            var expected = Context.Posts.FirstOrDefault(x => x.Id == postId);

            var response = await handler.Handle(
                new GetPostByIdQuery()
                {
                    Id = postId
                },
                CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.NotNull(response.Output);
            Assert.Equal(expected.Slug, response.Output.Slug);
            Assert.Equal(expected.Description, response.Output.Description);
            Assert.Equal(expected.ShortDescription, response.Output.ShortDescription);
            Assert.Equal(expected.CreatedDate, response.Output.CreatedDate);
            Assert.Equal(expected.Id, response.Output.Id);
            Assert.Equal(expected.Title, response.Output.Title);
            Assert.Equal(expected.ThumbnailUrl, response.Output.ThumbnailUrl);
        }

        [Fact]
        public async Task GetPostByIdQueryHandler_FailedWhenPostNotFound()
        {
            var handler = new GetPostByIdQueryHandler(Context, Mapper);
            var postId = 0;
            var expected = ErrorType.NotFound;

            var response = await handler.Handle(
                new GetPostByIdQuery()
                {
                    Id = postId
                },
                CancellationToken.None);

            Assert.False(response.Conclusion);
            Assert.Null(response.Output);
            Assert.Equal(expected, response.ErrorDescription.ErrorType);
        }
    }
}
