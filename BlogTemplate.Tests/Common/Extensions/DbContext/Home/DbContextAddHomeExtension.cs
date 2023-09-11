using BlogTemplate.Infrastructure.Data;

namespace BlogTemplate.Tests.Common.Extensions.DbContext.Home
{
    public static class DbContextAddHomeExtension
    {
        public static int PostsCount = 5;

        public static ApplicationDbContext AddHome(this ApplicationDbContext context)
        {
            var testUser = new Domain.Models.ApplicationUser()
            {
                Id = Guid.NewGuid().ToString()
            };
            testUser.Posts = new List<Domain.Models.Post>();
            for (var i = 0; i < PostsCount; i++)
            {
                testUser.Posts.Add(new Domain.Models.Post()
                {
                    Id = i + 1,
                    ApplicationUserId = testUser.Id,
                    CreatedDate = DateTime.Now,
                });
            }
            context.ApplicationUsers!.Add(testUser);
            foreach (var post in testUser.Posts)
            {
                context.Posts!.Add(post);
            }
            context.SaveChanges();
            return context;
        }
    }
}
