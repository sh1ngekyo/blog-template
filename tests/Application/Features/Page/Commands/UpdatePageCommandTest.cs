using BlogTemplate.Application.Abstractions.Enums;
using Xunit;
using BlogTemplate.Application.Features.Page.Commands.Update;
using Microsoft.EntityFrameworkCore;
using BlogTemplate.Application.Common.Behaviors;
using BlogTemplate.Application.Abstractions;
using BlogTemplate.Tests.Common;
using BlogTemplate.Tests.Common.Extensions.DbContext.Page;
using BlogTemplate.Tests.Common.Extensions.DbContext.Settings;

namespace BlogTemplate.Tests.Features.Page.Commands
{
    public class UpdatePageCommandTest : TestDbContextCommandBase
    {
        public UpdatePageCommandTest() 
            : base(DbContextBuilder.NewContext.AddPage().AddSettings())
        {
        }

        [Fact]
        public async Task UpdatePageCommandHandler_Success()
        {
            var updatedTitle = "New Title";

            var handler = new UpdatePageCommandHandler(Context);

            var response = await handler.Handle(new UpdatePageCommand
            {
                Id = DbContextAddPageExtension.PageId,
                Title = updatedTitle
            }, CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.Equal(ResultType.Updated, response.ResultType);
            Assert.Null(response.Output.RemoveThumbnailUrl);

            Assert.NotNull(await Context.Pages!.AsNoTracking().SingleOrDefaultAsync(page =>
                page.Id == DbContextAddPageExtension.PageId &&
                page.Title == updatedTitle));
        }

        [Fact]
        public async Task UpdatePageCommandHandler_SuccessReturnThumbnailUrlForReturn()
        {
            var oldThumbnail = (await Context.Pages!.SingleOrDefaultAsync(page =>
            page.Id == DbContextAddPageExtension.PageId))!.ThumbnailUrl;

            var handler = new UpdatePageCommandHandler(Context);

            var response = await handler.Handle(new UpdatePageCommand
            {
                Id = DbContextAddPageExtension.PageId,
                ThumbnailUrl = "New ThumbnailUrl"
            }, CancellationToken.None);

            Assert.True(response.Conclusion);
            Assert.Equal(ResultType.Updated, response.ResultType);
            Assert.NotNull(response.Output.RemoveThumbnailUrl);
            Assert.Equal(oldThumbnail, response.Output.RemoveThumbnailUrl);
            Assert.NotEqual(oldThumbnail, (await Context.Pages!.SingleOrDefaultAsync(page =>
                page.Id == DbContextAddPageExtension.PageId))!.ThumbnailUrl);
        }

        [Fact]
        public async Task UpdatePageCommandHandler_FailedOnWrongId()
        {
            var handler = new UpdatePageCommandHandler(Context);

            var response = await handler.Handle(new UpdatePageCommand
            {
                Id = DbContextAddPageExtension.PageId + 1
            }, CancellationToken.None);

            Assert.False(response.Conclusion);
            Assert.Equal(ErrorType.NotFound, response.ErrorDescription.ErrorType);
        }

        [Fact]
        public async Task UpdatePageCommandValidator_FailedOnWrongTitle()
        {
            var updatePageCommand = new UpdatePageCommand
            {
                Title = null
            };
            var updatePageCommandHandler = new UpdatePageCommandHandler(Context);

            var validationBehavior = new ValidationBehavior<UpdatePageCommand, Result<UpdatePageCommandResponse>>
            (new List<UpdatePageCommandValidator>()
            {
                new UpdatePageCommandValidator()
            });

            var response = await validationBehavior.Handle(updatePageCommand, async () =>
            {
                return await updatePageCommandHandler.Handle(updatePageCommand, CancellationToken.None);
            }, CancellationToken.None);


            Assert.False(response.Conclusion);
            Assert.Equal(ErrorType.NotValid, response.ErrorDescription.ErrorType);
        }
    }
}
