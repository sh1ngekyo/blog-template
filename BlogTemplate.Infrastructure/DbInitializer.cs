using BlogTemplate.Application.Abstractions;
using BlogTemplate.Domain;
using BlogTemplate.Domain.Models;
using BlogTemplate.Infrastructure.Data;

using Microsoft.AspNetCore.Identity;

namespace BlogTemplate.Infrastructure
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserManagerProxy<ApplicationUser> _userManager;
        private readonly IRoleManagerProxy<IdentityRole> _roleManager;
        public DbInitializer(ApplicationDbContext context,
                               IUserManagerProxy<ApplicationUser> userManager,
                               IRoleManagerProxy<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (!_roleManager.RoleExistsAsync(WebsiteRoles.WebsiteAdmin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAuthor)).GetAwaiter().GetResult();
                _userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "Admin",
                    Email = "admin@gmail.com",
                    FirstName = "Super",
                    LastName = "Admin"
                }, "Admin@0011").Wait();

                var appUser = _context.ApplicationUsers!.FirstOrDefault(x => x.Email == "admin@gmail.com");
                if (appUser != null)
                {
                    _userManager.AddToRoleAsync(appUser, WebsiteRoles.WebsiteAdmin).GetAwaiter().GetResult();
                }


                var listOfPages = new List<Page>()
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
                 };

                _context.Pages!.AddRange(listOfPages);

                var setting = new Setting
                {
                    SiteName = "Site Name",
                    Title = "Site Title",
                    ShortDescription = "Short Description of site"
                };

                _context.Settings!.Add(setting);
                _context.SaveChanges();

            }
        }
    }
}
