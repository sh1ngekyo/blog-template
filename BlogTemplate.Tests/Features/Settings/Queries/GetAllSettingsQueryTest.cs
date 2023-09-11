using AutoMapper;
using BlogTemplate.Application.Features.Settings.Queries.GetAll;
using BlogTemplate.Infrastructure.Data;

using Xunit;

namespace BlogTemplate.Tests.Features.Settings.Queries
{
    [Collection("SettingsQueryCollection")]
    public class GetAllSettingsQueryTest
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;

        public GetAllSettingsQueryTest(SettingsQueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetAllSettingsQueryHandler_Success()
        {
            var handler = new GetAllSettingsQueryHandler(Context, Mapper);

            var response = await handler.Handle(
                new GetAllSettingsQuery(),
                CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.NotNull(response.Output);
            Assert.NotEmpty(response.Output);
        }
    }
}
