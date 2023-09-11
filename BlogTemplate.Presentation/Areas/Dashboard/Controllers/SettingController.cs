using AspNetCoreHero.ToastNotification.Abstractions;
using BlogTemplate.Application.DataTransfer.Settings;
using BlogTemplate.Application.Features.Settings.Commands.Update;
using BlogTemplate.Application.Features.Settings.Queries.GetAll;
using BlogTemplate.Presentation.Abstractions.Controller;
using BlogTemplate.Presentation.Utills;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Presentation.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = "Admin")]
    public class SettingController : BaseController
    {
        private readonly ImageUtility _imageUtility;
        public INotyfService _notification { get; }

        public SettingController(INotyfService notyfService,
                                ImageUtility imageUtility)
        {
            _notification = notyfService;
            _imageUtility = imageUtility;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View((await Mediator.Send(new GetAllSettingsQuery())).Output.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Index(SettingsDto settingsDto)
        {
            if (!ModelState.IsValid) { return View(settingsDto); }
            if (settingsDto.Thumbnail != null)
            {
                settingsDto.ThumbnailUrl = _imageUtility.Upload(settingsDto.Thumbnail);
            }
            var response = await Mediator.Send(new UpdateSettingsCommand()
            {
                Id = settingsDto.Id,
                SiteName = settingsDto.SiteName,
                Title = settingsDto.Title,
                ShortDescription = settingsDto.ShortDescription,
                ThumbnailUrl = settingsDto.ThumbnailUrl,
                FacebookUrl = settingsDto.FacebookUrl,
                TwitterUrl = settingsDto.TwitterUrl,
                GithubUrl = settingsDto.GithubUrl,
            });
            if (response.Conclusion)
            {
                _imageUtility.Remove(response.Output.RemoveThumbnailUrl!);
                _notification.Success("Settings updated succesfully");
                return RedirectToAction("Index", "Setting", new { area = "Dashboard" });
            }
            _notification.Error($"Settings not found");
            return View(settingsDto);
        }
    }
}
