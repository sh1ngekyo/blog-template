using BlogTemplate.Infrastructure.Data;

namespace BlogTemplate.Tests.Common.Extensions.DbContext.Page
{
    public static class DbContextAddPageExtension
    {
        public static int PageId = 1;
        public static string PageSlug = "TestPage";

        public static ApplicationDbContext AddPage(this ApplicationDbContext context)
        {
            context.Pages!.Add(new Domain.Models.Page
            {
                ShortDescription = "ShortDescription",
                ThumbnailUrl = "ThumbnailUrl",
                Id = PageId,
                Description = "Description",
                Title = "Title",
                Slug = PageSlug,
            });
            context.SaveChanges();
            return context;
        }
    }
}
