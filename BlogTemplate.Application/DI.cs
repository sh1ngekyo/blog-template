using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Common.Behaviors;
using BlogTemplate.Application.Features;
using BlogTemplate.Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlogTemplate.Application
{
    public static class DI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly(), typeof(DI).Assembly });
            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));
            services.AddScoped<IUserManagerProxy<ApplicationUser>, UserManagerProxy<ApplicationUser>>();
            services.AddScoped<IRoleManagerProxy<IdentityRole>, RoleManagerProxy<IdentityRole>>();
            services.AddScoped<ISignInManagerProxy<ApplicationUser>, SignInManagerProxy<ApplicationUser>>();
            return services;
        }
    }
}
