using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Features.Post.Commands.Delete;
using BlogTemplate.Tests.Common;
using BlogTemplate.Tests.Common.Extensions.DbContext.Post;
using Xunit;

namespace BlogTemplate.Tests.Features.Post.Commands;

public class DeletePostCommandTest : TestUserManagerWithDbContextCommandBase
{
    public DeletePostCommandTest()
        : base(DbContextBuilder.NewContext.AddPosts(), UserManagerFactory.Create())
    {
    }

    [Fact]
    public async Task DeletePostCommandHandler_FailedWhenPostNotFound()
    {
        var handler = new DeletePostCommandHandler(_userManager, _context);

        var response = await handler.Handle(new DeletePostCommand
        {
            DeletedByUserName = UserManagerFactory.UserA,
            PostId = 0
        }, CancellationToken.None);

        Assert.False(response.Conclusion);
        Assert.Equal(ErrorType.NotFound, response.ErrorDescription?.ErrorType);
    }

    [Fact]
    public async Task DeletePostCommandHandler_SuccessWhenDeletedByAdmin()
    {
        var idForDelete = 1;
        var handler = new DeletePostCommandHandler(_userManager, _context);
        Assert.NotNull(_context.Posts?.FirstOrDefault(x => x.Id == idForDelete));

        var response = await handler.Handle(new DeletePostCommand
        {
            DeletedByUserName = UserManagerFactory.UserB,
            PostId = idForDelete
        }, CancellationToken.None);

        Assert.True(response.Conclusion);
        Assert.NotNull(response.Output?.RemoveThumbnailUrl);
        Assert.Equal(ResultType.Deleted, response.ResultType);

        var deletedPost = _context.Posts.FirstOrDefault(x => x.Id == idForDelete);
        Assert.Null(deletedPost);
    }

    [Fact]
    public async Task DeletePostCommandHandler_SuccessWhenDeletedByOwner()
    {
        var idForDelete = 2;
        var handler = new DeletePostCommandHandler(_userManager, _context);
        Assert.NotNull(_context.Posts?.FirstOrDefault(x => x.Id == idForDelete));

        var response = await handler.Handle(new DeletePostCommand
        {
            DeletedByUserName = UserManagerFactory.UserA,
            PostId = idForDelete
        }, CancellationToken.None);

        Assert.True(response.Conclusion);
        Assert.NotNull(response.Output?.RemoveThumbnailUrl);
        Assert.Equal(ResultType.Deleted, response.ResultType);

        var deletedPost = _context.Posts.FirstOrDefault(x => x.Id == idForDelete);
        Assert.Null(deletedPost);
    }

    [Fact]
    public async Task DeletePostCommandHandler_FailedWhenNotAuthorized()
    {
        var idForDelete = 3;
        var handler = new DeletePostCommandHandler(_userManager, _context);
        Assert.NotNull(_context.Posts?.FirstOrDefault(x => x.Id == idForDelete));

        var response = await handler.Handle(new DeletePostCommand
        {
            DeletedByUserName = UserManagerFactory.UserC,
            PostId = idForDelete
        }, CancellationToken.None);

        Assert.False(response.Conclusion);
        Assert.Equal(ErrorType.NotAuthorized, response.ErrorDescription?.ErrorType);
    }
}
