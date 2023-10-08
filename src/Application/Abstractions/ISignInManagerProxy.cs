namespace BlogTemplate.Application.Abstractions
{
    public interface ISignInManagerProxy<T> where T : class
    {
        Task<bool> PasswordSignInAsync(string username, string password, bool rememberMe, bool isLockout);

        Task SignOutAsync();
    }
}
