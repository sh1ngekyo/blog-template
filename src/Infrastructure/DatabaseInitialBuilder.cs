using BlogTemplate.Application.Abstractions;
using BlogTemplate.Domain;
using BlogTemplate.Domain.Models;
using BlogTemplate.Infrastructure.Data;

using Microsoft.AspNetCore.Identity;

namespace BlogTemplate.Infrastructure;

public class DatabaseInitialBuilder : IDatabaseInitialBuilder
{
    private readonly ApplicationDbContext _context;
    private readonly IUserManagerProxy<ApplicationUser> _userManager;
    private readonly IRoleManagerProxy<IdentityRole> _roleManager;
    public DatabaseInitialBuilder(ApplicationDbContext context,
                           IUserManagerProxy<ApplicationUser> userManager,
                           IRoleManagerProxy<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        if (!_roleManager.RoleExistsAsync(WebsiteRoles.WebsiteAdmin!).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAdmin!)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAuthor!)).GetAwaiter().GetResult();
        }
    }

    public IDatabaseInitialBuilder AddAdminUser()
    {
        var user = new ApplicationUser()
        {
            UserName = "Admin",
            Email = "admin@gmail.com",
            FirstName = "TestName",
            LastName = "TestSurname"
        };
        _userManager.CreateAsync(user, "Admin@1234").Wait();
        _userManager.AddToRoleAsync(user, WebsiteRoles.WebsiteAdmin!).GetAwaiter().GetResult();
        return this;
    }

    public IDatabaseInitialBuilder AddPages()
    {
        _context.Pages!.AddRange(new List<Page>()
        {
            new Page()
            {
                Title = "About Us",
                Slug = "about"
            },
            new Page()
            {
                Title = "Contact Us",
                Slug = "contact"
            },
            new Page()
            {
                Title = "Privacy Policy",
                Slug = "privacy"
            }
        });
        return this;
    }

    public IDatabaseInitialBuilder AddSettings()
    {
        _context.Settings!.Add(new Setting
        {
            SiteName = "Site Name",
            Title = "Site Title",
            ShortDescription = "Short Description of site"
        });
        return this;
    }

    public void Build()
    {
        _context.SaveChanges();
    }
}
