using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Common.Behaviors;
using BlogTemplate.Application.Features.Settings.Commands.Update;
using BlogTemplate.Tests.Common;
using BlogTemplate.Tests.Common.Extensions.DbContext.Settings;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogTemplate.Tests.Features.Settings.Commands
{

    public class UpdateSettingsCommandTest : TestDbContextCommandBase
    {
        public UpdateSettingsCommandTest()
            : base(DbContextBuilder.NewContext.AddSettings())
        {
        }

        [Fact]
        public async Task UpdateSettingsCommandHandler_Success()
        {
            var updatedTitle = "New Title";
            var handler = new UpdateSettingsCommandHandler(Context);

            var response = await handler.Handle(new UpdateSettingsCommand
            {
                Id = DbContextAddSettingsExtension.SettingsId,
                Title = updatedTitle
            }, CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.Equal(ResultType.Updated, response.ResultType);
            Assert.Null(response.Output.RemoveThumbnailUrl);

            Assert.NotNull(await Context.Settings!.SingleOrDefaultAsync(settings =>
                settings.Id == DbContextAddSettingsExtension.SettingsId &&
                settings.Title == updatedTitle));
        }

        [Fact]
        public async Task UpdateSettingsCommandHandler_SuccessReturnThumbnailUrlForReturn()
        {
            var oldThumbnail = (await Context.Settings!.SingleOrDefaultAsync(settings =>
            settings.Id == DbContextAddSettingsExtension.SettingsId))!.ThumbnailUrl;

            var handler = new UpdateSettingsCommandHandler(Context);

            var response = await handler.Handle(new UpdateSettingsCommand
            {
                Id = DbContextAddSettingsExtension.SettingsId,
                ThumbnailUrl = "New ThumbnailUrl"
            }, CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.Equal(ResultType.Updated, response.ResultType);
            Assert.NotNull(response.Output.RemoveThumbnailUrl);
            Assert.Equal(oldThumbnail, response.Output.RemoveThumbnailUrl);
            Assert.NotEqual(oldThumbnail, (await Context.Settings!.SingleOrDefaultAsync(settings =>
                settings.Id == DbContextAddSettingsExtension.SettingsId))!.ThumbnailUrl);
        }

        [Fact]
        public async Task UpdateSettingsCommandHandler_FailedOnWrongId()
        {
            var handler = new UpdateSettingsCommandHandler(Context);

            var response = await handler.Handle(new UpdateSettingsCommand
            {
                Id = DbContextAddSettingsExtension.SettingsId + 1
            }, CancellationToken.None);

            Assert.False(response.Conclusion);
            Assert.Equal(ErrorType.NotFound, response.ErrorDescription.ErrorType);
        }

        [Fact]
        public async Task UpdateSettingsCommandValidator_FailedOnWrongSiteName()
        {
            var updateSettingsCommand = new UpdateSettingsCommand
            {
                SiteName = null
            };

            var updateSettingsCommandHandler = new UpdateSettingsCommandHandler(Context);

            var validationBehavior = new ValidationBehavior<UpdateSettingsCommand, Result>
            (new List<UpdateSettingsCommandValidator>()
            {
                new UpdateSettingsCommandValidator()
            });

            var response = await validationBehavior.Handle(updateSettingsCommand, async () =>
            {
                return await updateSettingsCommandHandler.Handle(updateSettingsCommand, CancellationToken.None);
            }, CancellationToken.None);


            Assert.False(response.Conclusion);
            Assert.Equal(ErrorType.NotValid, response.ErrorDescription.ErrorType);
        }
    }
}
