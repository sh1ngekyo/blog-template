using BlogTemplate.Application.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace BlogTemplate.Application.Features
{
    public class RoleManagerProxy <T> : IRoleManagerProxy<T> where T : class
    {
        private readonly RoleManager<T> _roleManager;

        public RoleManagerProxy(RoleManager<T> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task CreateAsync(T role)
        {
            await _roleManager.CreateAsync(role);
        }

        public async Task<bool> RoleExistsAsync(string role)
        {
            return await _roleManager.RoleExistsAsync(role);
        }
    }
}
