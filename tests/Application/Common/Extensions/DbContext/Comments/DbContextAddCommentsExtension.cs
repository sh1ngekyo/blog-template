using BlogTemplate.Infrastructure.Data;

namespace BlogTemplate.Tests.Common.Extensions.DbContext.Comments
{
    public static class DbContextAddCommentsExtension
    {
        public static ApplicationDbContext AddComments(this ApplicationDbContext context)
        {
            context.ApplicationUsers.AddRange(
                new Domain.Models.ApplicationUser
                {
                    UserName = UserManagerFactory.UserA,
                    Id = UserManagerFactory.UserAId,
                },
                new Domain.Models.ApplicationUser
                {
                    UserName = UserManagerFactory.UserB,
                    Id = UserManagerFactory.UserBId,
                },
                new Domain.Models.ApplicationUser
                {
                    UserName = UserManagerFactory.UserC,
                    Id = UserManagerFactory.UserCId,
                });
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
                }
            );
            var date = DateTimeOffset.Now;
            context.Comments.AddRange(
                new Domain.Models.Comment
                {
                    ApplicationUserId = UserManagerFactory.UserAId,
                    ParentId = null,
                    PostId = 1,
                    Content = "Comment 1",
                    DateCreated = date,
                    DateModified = date,
                }, new Domain.Models.Comment
                {
                    ApplicationUserId = UserManagerFactory.UserBId,
                    ParentId = null,
                    PostId = 1,
                    Content = "Comment 2",
                    DateCreated = date,
                    DateModified = date,
                }
            );
            context.SaveChanges();
            foreach (var comment in context.Comments.ToList())
            {
                context.Comments.Add(new Domain.Models.Comment
                {
                    ApplicationUserId = UserManagerFactory.UserCId,
                    ParentId = comment.CommentId,
                    PostId = 1,
                    Content = $"Comment {comment.CommentId} reply",
                    DateCreated = date,
                    DateModified = date,
                });
            }
            context.SaveChanges();
            return context;
        }
    }
}
