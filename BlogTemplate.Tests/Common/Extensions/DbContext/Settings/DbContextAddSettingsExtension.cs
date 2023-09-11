using BlogTemplate.Infrastructure.Data;

namespace BlogTemplate.Tests.Common.Extensions.DbContext.Settings
{
    public static class DbContextAddSettingsExtension
    {
        public static int SettingsId = 1;

        public static ApplicationDbContext AddSettings(this ApplicationDbContext context)
        {
            context.Settings.AddRange(
                new Domain.Models.Setting
                {
                    Id = SettingsId,
                    Title = "Title",
                    SiteName = "SiteName",
                    ShortDescription = "ShortDescription",
                    ThumbnailUrl = "ThumbnailUrl",
                    GithubUrl = "GithubUrl",
                    FacebookUrl = "FacebookUrl",
                    TwitterUrl = "TwitterUrl",
                }
            );
            context.SaveChanges();
            return context;
        }
    }
}
