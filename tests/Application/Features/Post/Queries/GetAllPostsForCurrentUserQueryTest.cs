using AutoMapper;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Features.Post.Queries.GetAllForCurrentUser;
using BlogTemplate.Domain.Models;
using BlogTemplate.Infrastructure.Data;
using BlogTemplate.Tests.Common;

using Xunit;

namespace BlogTemplate.Tests.Features.Post.Queries;

[Collection("PostQueryCollection")]
public class GetAllPostsForCurrentUserQueryTest
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;
    private readonly IUserManagerProxy<ApplicationUser> _userManager;
    public GetAllPostsForCurrentUserQueryTest(PostQueryTestFixture fixture)
    {
        _userManager = fixture.UserManager;
        _mapper = fixture.Mapper;
        _context = fixture.Context;
    }

    [Fact]
    public async Task GetAllPostsForCurrentUserQueryHandler_SuccessReturnAllForAdmin()
    {
        var handler = new GetAllPostsForCurrentUserQueryHandler(_userManager, _context, _mapper);

        var response = await handler.Handle(
            new GetAllPostsForCurrentUserQuery()
            {
                UserName = UserManagerFactory.UserB
            },
            CancellationToken.None);

        Assert.True(response.Conclusion);
        Assert.NotNull(response.Output);
        Assert.Equal(_context.Posts!.Count(), response.Output.Count);
    }

    [Fact]
    public async Task GetAllPostsForCurrentUserQueryHandler_SuccessReturnOnlyOwnedForAuthor()
    {
        var handler = new GetAllPostsForCurrentUserQueryHandler(_userManager, _context, _mapper);

        var response = await handler.Handle(
            new GetAllPostsForCurrentUserQuery()
            {
                UserName = UserManagerFactory.UserA
            },
            CancellationToken.None);

        Assert.True(response.Conclusion);
        Assert.NotNull(response.Output);
        Assert.Equal(
            _context.Posts!.Where(x => x.ApplicationUserId == UserManagerFactory.UserAId).Count(),
            response.Output.Count);
    }
}
