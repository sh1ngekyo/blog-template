using BlogTemplate.Application.Abstractions;
using BlogTemplate.Domain.Models;
using BlogTemplate.Tests.Common.Extensions.Queriable;

using NSubstitute;

namespace BlogTemplate.Tests.Common;

public class UserManagerFactory
{
    public static readonly string UserA = "UserA";
    public static readonly string UserAId = "1";
    public static readonly string UserARole = "Author";
    public static readonly string UserB = "UserB";
    public static readonly string UserBId = "2";
    public static readonly string UserBRole = "Admin";
    public static readonly string UserC = "UserC";
    public static readonly string UserCId = "3";
    public static readonly string UserCRole = "Author";

    private static readonly Dictionary<string, ApplicationUser> Users = new()
    {
        {
            UserA,
            new ApplicationUser
            {
                Id = UserAId,
                UserName = UserA,
                FirstName = UserA,
                LastName = "LastName",
                About = "About",
                ThumbnailUrl = "ThumbnailUrl",
            }
        },
        {
            UserB,
            new ApplicationUser
            {
                Id = UserBId,
                UserName = UserB,
                FirstName = UserB,
                LastName = "LastName",
                About = "About",
                ThumbnailUrl = "ThumbnailUrl",
            }
        },
        {
            UserC,
            new ApplicationUser
            {
                Id = UserCId,
                UserName = UserC,
                FirstName = UserC,
                LastName = "LastName",
                About = "About",
                ThumbnailUrl = "ThumbnailUrl",
            }
        }
    };

    public static IUserManagerProxy<ApplicationUser> Create()
    {
        var userManager = Substitute.For<IUserManagerProxy<ApplicationUser>>();
        userManager.Users.Returns(Users.Values.ToList().AsAsyncQueryable());
        userManager.GetRolesAsync(Arg.Any<ApplicationUser>())
            .Returns(x => (x[0] as ApplicationUser)!.UserName == UserB
                    ? new List<string>()
                    {
                        UserBRole
                    }
                    : (IList<string>)new List<string>()
                    {
                        UserARole
                    });
        userManager.AddToRoleAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(true);
        userManager.RemoveFromRoleAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(true);
        userManager.FindByUserNameAsync(Arg.Any<string>()).Returns(x => 
            Users.TryGetValue(x[0].ToString()!, out var user) ? user : null);
        userManager.FindByIdAsync(Arg.Any<string>()).Returns(x => 
            Users.FirstOrDefault(item => item.Value.Id == x[0].ToString()).Value);
        userManager.DeleteAsync(Arg.Any<ApplicationUser>()).Returns(true);
        userManager.UpdateAsync(Arg.Any<ApplicationUser>()).Returns(true);
        userManager.GeneratePasswordResetTokenAsync(Arg.Any<ApplicationUser>()).Returns(Guid.NewGuid().ToString());
        userManager.ResetPasswordAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>(), Arg.Any<string>()).Returns(true);
        return userManager;
    }
}
