using BlogTemplate.Application.Abstractions;
using BlogTemplate.Domain.Models;
using BlogTemplate.Infrastructure.Data;

namespace BlogTemplate.Tests.Common;

public class TestUserManagerWithDbContextCommandBase : IDisposable
{
    protected readonly ApplicationDbContext _context;
    protected readonly IUserManagerProxy<ApplicationUser> _userManager;

    public TestUserManagerWithDbContextCommandBase(ApplicationDbContext context, IUserManagerProxy<ApplicationUser> userManagerProxy)
    {
        _context = context.Build();
        _userManager = userManagerProxy;
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
