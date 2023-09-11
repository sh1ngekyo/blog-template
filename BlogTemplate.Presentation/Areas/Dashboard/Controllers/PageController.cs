using AspNetCoreHero.ToastNotification.Abstractions;

using BlogTemplate.Application.DataTransfer.Page;
using BlogTemplate.Application.Features.Page.Commands.Update;
using BlogTemplate.Application.Features.Page.Queries.GetBySlug;
using BlogTemplate.Presentation.Abstractions.Controller;
using BlogTemplate.Presentation.Utills;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Presentation.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = "Admin")]
    public class PageController : BaseController
    {
        public INotyfService _notification { get; }
        private readonly ImageUtility _imageUtility;

        public PageController(INotyfService notification,
                                ImageUtility imageUtility)
        {
            _notification = notification;
            _imageUtility = imageUtility;
        }

        [HttpGet]
        public async Task<IActionResult> About()
        {
            var response = await Mediator.Send(new GetPageBySlugQuery()
            {
                Slug = "about"
            });
            return View(response.Output);
        }

        [HttpPost]
        public async Task<IActionResult> About(PageDto pageDto)
        {
            if (pageDto.Thumbnail != null)
            {
                pageDto.ThumbnailUrl = _imageUtility.Upload(pageDto.Thumbnail);
            }
            var response = await Mediator.Send(new UpdatePageCommand()
            {
                Id = pageDto.Id,
                Title = pageDto.Title,
                Description = pageDto.Description,
                ShortDescription = pageDto.ShortDescription,
                ThumbnailUrl = pageDto.ThumbnailUrl,
            });
            if (response.Conclusion)
            {
                _imageUtility.Remove(response.Output.RemoveThumbnailUrl);
                _notification.Success("About page updated succesfully");
                return RedirectToAction("About", "Page", new { area = "Dashboard" });
            }
            _notification.Error(response.ErrorDescription.ErrorMessage);
            return View(pageDto);
        }

        [HttpGet]
        public async Task<IActionResult> Contact()
        {
            var response = await Mediator.Send(new GetPageBySlugQuery()
            {
                Slug = "contact"
            });
            return View(response.Output);
        }

        [HttpPost]
        public async Task<IActionResult> Contact(PageDto pageDto)
        {
            if (pageDto.Thumbnail != null)
            {
                pageDto.ThumbnailUrl = _imageUtility.Upload(pageDto.Thumbnail);
            }
            var response = await Mediator.Send(new UpdatePageCommand()
            {
                Id = pageDto.Id,
                Title = pageDto.Title,
                Description = pageDto.Description,
                ShortDescription = pageDto.ShortDescription,
                ThumbnailUrl = pageDto.ThumbnailUrl,
            });
            if (response.Conclusion)
            {
                _imageUtility.Remove(response.Output.RemoveThumbnailUrl);
                _notification.Success("Contact page updated succesfully");
                return RedirectToAction("Contact", "Page", new { area = "Dashboard" });
            }
            _notification.Error(response.ErrorDescription.ErrorMessage);
            return View(pageDto);
        }


        [HttpGet]
        public async Task<IActionResult> Privacy()
        {
            var response = await Mediator.Send(new GetPageBySlugQuery()
            {
                Slug = "privacy"
            });
            return View(response.Output);
        }

        [HttpPost]
        public async Task<IActionResult> Privacy(PageDto pageDto)
        {
            if (pageDto.Thumbnail != null)
            {
                pageDto.ThumbnailUrl = _imageUtility.Upload(pageDto.Thumbnail);
            }
            var response = await Mediator.Send(new UpdatePageCommand()
            {
                Id = pageDto.Id,
                Title = pageDto.Title,
                Description = pageDto.Description,
                ShortDescription = pageDto.ShortDescription,
                ThumbnailUrl = pageDto.ThumbnailUrl,
            });
            if (response.Conclusion)
            {
                _imageUtility.Remove(response.Output.RemoveThumbnailUrl);
                _notification.Success("Privacy page updated succesfully");
                return RedirectToAction("Privacy", "Page", new { area = "Dashboard" });
            }
            _notification.Error(response.ErrorDescription.ErrorMessage);
            return View(pageDto);
        }
    }
}
