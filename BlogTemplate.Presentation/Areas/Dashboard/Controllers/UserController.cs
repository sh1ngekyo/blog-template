using AspNetCoreHero.ToastNotification.Abstractions;

using BlogTemplate.Application.DataTransfer.Auth;
using BlogTemplate.Application.Features.Auth.Commands.SignIn;
using BlogTemplate.Application.Features.Auth.Commands.SignOut;
using BlogTemplate.Application.Features.Auth.Commands.SignUp;
using BlogTemplate.Application.Features.User.Commands.ChangeRole;
using BlogTemplate.Application.Features.User.Commands.Delete;
using BlogTemplate.Application.Features.User.Queries.GetAll;
using BlogTemplate.Presentation.Abstractions.Controller;
using BlogTemplate.Presentation.Utills;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Presentation.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class UserController : BaseController
    {
        private readonly ImageUtility _imageUtility;
        public INotyfService _notification { get; }
        public UserController(INotyfService notyfService, ImageUtility imageUtility)
        {
            _notification = notyfService;
            _imageUtility = imageUtility;

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await Mediator.Send(new GetAllUsersQuery());
            return View(response.Output.Where(x => x.UserName != User.Identity!.Name).ToList());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await Mediator.Send(new DeleteUserCommand
            {
                Id = id
            });
            if (!response.Conclusion)
            {
                _notification.Error("User doesn't exsits");
            }
            else
            {
                _imageUtility.Remove(response.Output.RemoveThumbnailUrl);
                _notification.Success($"User {response.Output.DeletedUserName!} has been deleted");
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ChangeRole(string id, bool isAdmin)
        {
            var response = await Mediator.Send(new ChangeUserRoleCommand
            {
                Id = id,
                IsAdmin = isAdmin
            });
            if (!response.Conclusion)
            {
                _notification.Error("User doesnot exsits");
                return RedirectToAction("Index");
            }
            _notification.Success(response.Output.ResultMessage);
            return RedirectToAction("Index");
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            if (!HttpContext.User.Identity!.IsAuthenticated)
            {
                return View(new SignUpDto());
            }
            return RedirectToAction("Index", "Post", new { area = "Dashboard" });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(SignUpDto signUpDto)
        {
            if (!ModelState.IsValid) { return View(signUpDto); }
            var response = await Mediator.Send(new SignUpCommand
            {
                Email = signUpDto.Email,
                UserName = signUpDto.UserName,
                FirstName = signUpDto.FirstName,
                LastName = signUpDto.LastName,
                Password = signUpDto.Password,
            });
            if (!response.Conclusion)
            {
                _notification.Error(response.ErrorDescription.ErrorMessage);
                return View(signUpDto);
            }
            _notification.Success("Registration completed");
            return RedirectToAction("Index", "Post", new { area = "Dashboard" });
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            if (!HttpContext.User.Identity!.IsAuthenticated)
            {
                return View(new SignInDto());
            }
            return RedirectToAction("Index", "Post", new { area = "Dashboard" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(SignInDto signInDto)
        {
            if (!ModelState.IsValid) { return View(signInDto); }
            var response = await Mediator.Send(new SignInCommand
            {
                Username = signInDto.Username,
                Password = signInDto.Password,
            });
            if (!response.Conclusion)
            {
                _notification.Error(response.ErrorDescription.ErrorMessage);
                return View(signInDto);
            }
            _notification.Success("Login Successful");
            return RedirectToAction("Index", "Post", new { area = "Dashboard" });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await Mediator.Send(new SignOutCommand());
            _notification.Success("You are logged out successfully");
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet("AccessDenied")]
        [Authorize]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
