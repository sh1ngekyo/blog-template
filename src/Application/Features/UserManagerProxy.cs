using BlogTemplate.Application.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace BlogTemplate.Application.Features
{
    public class UserManagerProxy<T> : IUserManagerProxy<T> where T : class
    {
        private readonly UserManager<T> _userManager;

        public IQueryable<T?> Users => _userManager.Users;

        public UserManagerProxy(UserManager<T> userManager) 
        {
            _userManager = userManager;
        }

        public async Task<bool> AddToRoleAsync(T user, string role) 
            => (await _userManager.AddToRoleAsync(user, role)).Succeeded;

        public async Task<bool> CheckPasswordAsync(T user, string password) 
            => await _userManager.CheckPasswordAsync(user, password);

        public async Task<bool> CreateAsync(T user, string password) 
            => (await _userManager.CreateAsync(user, password)).Succeeded;

        public async Task<bool> DeleteAsync(T user) => 
            (await _userManager.DeleteAsync(user)).Succeeded;

        public async Task<T?> FindByEmailAsync(string email) 
            => await _userManager.FindByEmailAsync(email);

        public async Task<T?> FindByIdAsync(string id) 
            => await _userManager.FindByIdAsync(id);

        public async Task<T?> FindByUserNameAsync(string userName) 
            => await _userManager.FindByNameAsync(userName);

        public async Task<string> GeneratePasswordResetTokenAsync(T user) 
            => await _userManager.GeneratePasswordResetTokenAsync(user);

        public async Task<IList<string>> GetRolesAsync(T user) 
            => await _userManager.GetRolesAsync(user);

        public async Task<bool> RemoveFromRoleAsync(T user, string role) 
            => (await _userManager.RemoveFromRoleAsync(user, role)).Succeeded;

        public async Task<bool> ResetPasswordAsync(T user, string token, string newPassword) 
            => (await _userManager.ResetPasswordAsync(user, token, newPassword)).Succeeded;

        public async Task<bool> UpdateAsync(T user) 
            => (await _userManager.UpdateAsync(user)).Succeeded;

        public void Dispose() 
            => _userManager.Dispose();
    }
}
