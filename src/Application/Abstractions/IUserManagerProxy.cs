namespace BlogTemplate.Application.Abstractions
{
    public interface IUserManagerProxy<T> : IDisposable where T : class
    {
        IQueryable<T?> Users { get; }
        Task<T?> FindByEmailAsync(string email);
        Task<T?> FindByUserNameAsync(string userName);
        Task<T?> FindByIdAsync(string id);
        Task<bool> CreateAsync(T user, string password);
        Task<bool> AddToRoleAsync(T user, string role);
        Task<bool> RemoveFromRoleAsync(T user, string role);
        Task<IList<string>> GetRolesAsync(T user);
        Task<string> GeneratePasswordResetTokenAsync(T user);
        Task<bool> ResetPasswordAsync(T user, string token, string newPassword);
        Task<bool> CheckPasswordAsync(T user, string password);
        Task<bool> UpdateAsync(T user);
        Task<bool> DeleteAsync(T user);
    }
}
