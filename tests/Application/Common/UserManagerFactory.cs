using BlogTemplate.Application.Abstractions;
using BlogTemplate.Domain.Models;
using BlogTemplate.Tests.Common.Extensions.Queriable;

using NSubstitute;

namespace BlogTemplate.Tests.Common
{
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

        private static Dictionary<string, ApplicationUser> Users = new Dictionary<string, ApplicationUser>()
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
            var _userManager = Substitute.For<IUserManagerProxy<ApplicationUser>>();
            _userManager.Users.Returns(Users.Values.ToList().AsAsyncQueryable());
            _userManager.GetRolesAsync(Arg.Any<ApplicationUser>())
                .Returns(x =>
                {
                    if ((x[0] as ApplicationUser)!.UserName == UserB)
                    {
                        return new List<string>()
                        {
                            UserBRole
                        };
                    }
                    return new List<string>()
                        {
                            UserARole
                        };
                });
            _userManager.AddToRoleAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(true);
            _userManager.RemoveFromRoleAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(true);
            _userManager.FindByUserNameAsync(Arg.Any<string>()).Returns(x =>
            {
                if (Users.TryGetValue(x[0].ToString()!, out var user))
                {
                    return user;
                }
                return null;
            });
            _userManager.FindByIdAsync(Arg.Any<string>()).Returns(x =>
            {
                return Users.FirstOrDefault(item => item.Value.Id == x[0].ToString()).Value;
            });
            _userManager.DeleteAsync(Arg.Any<ApplicationUser>()).Returns(true);
            _userManager.UpdateAsync(Arg.Any<ApplicationUser>()).Returns(true);
            _userManager.GeneratePasswordResetTokenAsync(Arg.Any<ApplicationUser>()).Returns(Guid.NewGuid().ToString());
            _userManager.ResetPasswordAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>(), Arg.Any<string>()).Returns(true);
            return _userManager;
        }
    }
}
