using BlogTemplate.Infrastructure.Data;

namespace BlogTemplate.Tests.Common.Extensions.DbContext.Post
{
    public static class DbContextAddPostsExtension
    {
        public static ApplicationDbContext AddPosts(this ApplicationDbContext context)
        {
            context.Posts.AddRange(
                new Domain.Models.Post
                {
                    Id = 1,
                    Title = "Title",
                    CreatedDate = DateTime.Now,
                    Description = "Description",
                    ShortDescription = "ShortDescription",
                    Slug = "Post1",
                    ThumbnailUrl = "ThumbnailUrl",
                    ApplicationUserId = UserManagerFactory.UserAId,
                    Comments = null
                },
                new Domain.Models.Post
                {
                    Id = 2,
                    Title = "Title",
                    CreatedDate = DateTime.Now,
                    Description = "Description",
                    ShortDescription = "ShortDescription",
                    Slug = "Post2",
                    ThumbnailUrl = "ThumbnailUrl",
                    ApplicationUserId = UserManagerFactory.UserAId,
                    Comments = null
                },
                new Domain.Models.Post
                {
                    Id = 3,
                    Title = "Title",
                    CreatedDate = DateTime.Now,
                    Description = "Description",
                    ShortDescription = "ShortDescription",
                    Slug = "Post3",
                    ThumbnailUrl = "ThumbnailUrl",
                    ApplicationUserId = UserManagerFactory.UserAId,
                    Comments = null
                },
                new Domain.Models.Post
                {
                    Id = 4,
                    Title = "Title",
                    CreatedDate = DateTime.Now,
                    Description = "Description",
                    ShortDescription = "ShortDescription",
                    Slug = "Post4",
                    ThumbnailUrl = "ThumbnailUrl",
                    ApplicationUserId = UserManagerFactory.UserC,
                    Comments = null
                }
            );
            context.SaveChanges();
            return context;
        }
    }
}
