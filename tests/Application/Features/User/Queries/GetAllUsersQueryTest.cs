using AutoMapper;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Features.User.Queries.GetAll;
using BlogTemplate.Domain.Models;
using BlogTemplate.Tests.Common;
using Xunit;

namespace BlogTemplate.Tests.Features.User.Queries;

[Collection("UserQueryCollection")]
public class GetAllUsersQueryTest
{
    private readonly IMapper _mapper;
    private readonly IUserManagerProxy<ApplicationUser> _userManager;
    public GetAllUsersQueryTest(UserQueryTestFixture fixture)
    {
        _userManager = fixture.UserManager;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task GetAllUsersQueryHandler_Success()
    {
        var handler = new GetAllUsersQueryHandler(_userManager, _mapper);

        var response = await handler.Handle(
            new GetAllUsersQuery(),
            CancellationToken.None);

        Assert.True(response.Conclusion);
        Assert.NotNull(response.Output);
        Assert.NotEmpty(response.Output);
        Assert.Equal(UserManagerFactory.UserA, response.Output[0].UserName);
        Assert.Equal(UserManagerFactory.UserARole, response.Output[0].Role);
        Assert.Equal(UserManagerFactory.UserB, response.Output[1].UserName);
        Assert.Equal(UserManagerFactory.UserBRole, response.Output[1].Role);
    }
}
