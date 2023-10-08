using Microsoft.EntityFrameworkCore;

namespace BlogTemplate.Application.Abstractions.Database
{
    public interface IApplicationDbContext : IDisposable
    {
        public DbSet<Domain.Models.ApplicationUser>? ApplicationUsers { get; set; }
        public DbSet<Domain.Models.Post>? Posts { get; set; }
        public DbSet<Domain.Models.Page>? Pages { get; set; }
        public DbSet<Domain.Models.Setting>? Settings { get; set; }
        public DbSet<Domain.Models.Comment>? Comments { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
