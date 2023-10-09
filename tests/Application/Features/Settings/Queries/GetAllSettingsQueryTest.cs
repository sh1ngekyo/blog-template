using AutoMapper;
using BlogTemplate.Application.Features.Settings.Queries.GetAll;
using BlogTemplate.Infrastructure.Data;

using Xunit;

namespace BlogTemplate.Tests.Features.Settings.Queries;

[Collection("SettingsQueryCollection")]
public class GetAllSettingsQueryTest
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllSettingsQueryTest(SettingsQueryTestFixture fixture)
    {
        _context = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task GetAllSettingsQueryHandler_Success()
    {
        var handler = new GetAllSettingsQueryHandler(_context, _mapper);

        var response = await handler.Handle(
            new GetAllSettingsQuery(),
            CancellationToken.None);

        Assert.True(response.Conclusion);
        Assert.NotNull(response.Output);
        Assert.NotEmpty(response.Output);
    }
}
