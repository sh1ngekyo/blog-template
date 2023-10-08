using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogTemplate.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>(comment =>
            {
                comment.HasKey(c => c.CommentId);
                comment.HasIndex(c => c.ParentId);

                comment.HasOne(c => c.Parent)
                       .WithMany(c => c.Children)
                       .HasForeignKey(c => c.ParentId);

                comment.Property(c => c.PostId).IsRequired();
                comment.Property(c => c.ApplicationUserId).IsRequired();
                comment.Property(c => c.Content).HasMaxLength(1000).IsRequired();
            });
        }

        public DbSet<ApplicationUser>? ApplicationUsers { get; set; }
        public DbSet<Post>? Posts { get; set; }
        public DbSet<Page>? Pages { get; set; }
        public DbSet<Setting>? Settings { get; set; }
        public DbSet<Comment>? Comments { get; set; }
    }
}
