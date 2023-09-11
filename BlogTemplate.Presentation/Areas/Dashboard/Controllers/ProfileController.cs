using AspNetCoreHero.ToastNotification.Abstractions;

using BlogTemplate.Application.DataTransfer.Profile;
using BlogTemplate.Application.Features.Profile.Commands.ResetPassword;
using BlogTemplate.Application.Features.Profile.Commands.Update;
using BlogTemplate.Application.Features.Profile.Queries.GetMyProfileByName;
using BlogTemplate.Application.Features.User.Queries.GetByName;
using BlogTemplate.Presentation.Abstractions.Controller;
using BlogTemplate.Presentation.Utills;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Presentation.Areas.Dashboard.Controllers
{
    [Authorize]
    [Area("Dashboard")]
    public class ProfileController : BaseController
    {
        private readonly ImageUtility _imageUtility;
        public INotyfService _notification { get; }
        public ProfileController(ImageUtility imageUtility,
                                    INotyfService notyfService)
        {
            _imageUtility = imageUtility;
            _notification = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await Mediator.Send(new GetMyProfileByNameQuery
            {
                UserName = User.Identity!.Name
            });
            return View(response.Output);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userResponse = await Mediator.Send(new GetUserByNameQuery
            {
                UserName = User.Identity!.Name
            });

            var profileEditDto = new ProfileEditDto()
            {
                FirstName = userResponse.Output.FirstName,
                LastName = userResponse.Output.LastName,
                UserName = userResponse.Output.UserName,
                Email = userResponse.Output.Email,
                About = userResponse.Output.About,
                ThumbnailUrl = userResponse.Output.ThumbnailUrl,
            };

            return View(profileEditDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileEditDto profileEditDto)
        {
            if (!ModelState.IsValid) { return View(profileEditDto); }
            if (profileEditDto.Thumbnail != null)
            {
                profileEditDto.ThumbnailUrl = _imageUtility.Upload(profileEditDto.Thumbnail);
            }
            var response = await Mediator.Send(new UpdateProfileCommand()
            {
                UserName = User.Identity!.Name,
                ThumbnailUrl = profileEditDto.ThumbnailUrl,
                About = profileEditDto.About,
                Email = profileEditDto.Email,
                FirstName = profileEditDto.FirstName,
                LastName = profileEditDto.LastName,
            });
            if (response.Conclusion)
            {
                _imageUtility.Remove(response.Output.RemoveThumbnailUrl);
                _notification.Success("Profile updated succuful");
                return RedirectToAction(nameof(Index));
            }
            _notification.Error(response.ErrorDescription.ErrorMessage);
            return View(profileEditDto);
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword()
        {
            var userResponse = await Mediator.Send(new GetUserByNameQuery
            {
                UserName = User.Identity!.Name
            });
            if (!userResponse.Conclusion)
            {
                _notification.Error("User doesnot exsits");
                return View();
            }
            var resetPasswordDto = new ResetPasswordDto()
            {
                UserName = userResponse.Output.UserName
            };
            return View(resetPasswordDto);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid) { return View(resetPasswordDto); }
            var response = await Mediator.Send(new ResetPasswordCommand
            {
                UserName = User.Identity!.Name,
                NewPassword = resetPasswordDto.NewPassword,
            });
            if (response.Conclusion)
            {
                _notification.Success("Password reset succuful");
                return RedirectToAction(nameof(Index));
            }
            _notification.Error(response.ErrorDescription.ErrorMessage);
            return View(resetPasswordDto);
        }
    }
}
