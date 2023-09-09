using BlogTemplate.Domain.Models;
using BlogTemplate.Infrastructure.Data;
using BlogTemplate.Infrastructure;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using BlogTemplate.Application;
using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Common.Mappings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetRequiredService<ApplicationDbContext>());

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IApplicationDbContext).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/AccessDenied";
});

var app = builder.Build();
DataSeeding();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();

void DataSeeding()
{
    using (var scope = app.Services.CreateScope())
    {
        var DbInitialize = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        DbInitialize.Initialize();
    }
}

