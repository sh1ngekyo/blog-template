namespace BlogTemplate.Application.Abstractions
{
    public interface IRoleManagerProxy<T> where T : class
    {
        Task CreateAsync(T role);
        Task<bool> RoleExistsAsync(string role);
    }
}
