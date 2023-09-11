using AspNetCoreHero.ToastNotification.Abstractions;
using BlogTemplate.Application.DataTransfer.Post;
using BlogTemplate.Application.Features.Post.Commands.Create;
using BlogTemplate.Application.Features.Post.Commands.Delete;
using BlogTemplate.Application.Features.Post.Commands.Update;
using BlogTemplate.Application.Features.Post.Queries.GetAllForCurrentUser;
using BlogTemplate.Application.Features.Post.Queries.GetById;
using BlogTemplate.Application.Features.User.Queries.GetByName;
using BlogTemplate.Domain;
using BlogTemplate.Presentation.Abstractions.Controller;
using BlogTemplate.Presentation.Utills;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using X.PagedList;

namespace BlogTemplate.Presentation.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class PostController : BaseController
    {
        private readonly ImageUtility _imageUtility;
        public INotyfService _notification { get; }

        public PostController(INotyfService notyfService,
                                ImageUtility imageUtility)
        {
            _notification = notyfService;
            _imageUtility = imageUtility;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            var response = await Mediator.Send(new GetAllPostsForCurrentUserQuery()
            {
                UserName = User.Identity!.Name
            });

            var pageSize = 5;
            var pageNumber = (page ?? 1);

            return View(
                await response.Output
                .OrderByDescending(x => x.CreatedDate)
                .ToPagedListAsync(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreatePostDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDto createPostDto)
        {
            if (!ModelState.IsValid) { return View(createPostDto); }
            string? thumbnailUrl = null;
            if (createPostDto.Thumbnail != null)
            {
                thumbnailUrl = _imageUtility.Upload(createPostDto.Thumbnail);
            }
            await Mediator.Send(new CreatePostCommand
            {
                UserName = User.Identity!.Name,
                Title = createPostDto.Title,
                Description = createPostDto.Description,
                ShortDescription = createPostDto.ShortDescription,
                ThumbnailUrl = thumbnailUrl,
            });
            _notification.Success("Post Created Successfully");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await Mediator.Send(new DeletePostCommand
            {
                PostId = id,
                DeletedByUserName = User.Identity!.Name
            });
            if (!response.Conclusion)
            {
                _notification.Error(response.ErrorDescription.ErrorMessage);
                return RedirectToAction("Index");

            }
            _imageUtility.Remove(response.Output.RemoveThumbnailUrl);
            _notification.Success("Post Deleted Successfully");
            return RedirectToAction("Index", "Post", new { area = "Dashboard" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var postResponse = await Mediator.Send(new GetPostByIdQuery { Id = id });
            if (!postResponse.Conclusion)
            {
                _notification.Error(postResponse.ErrorDescription.ErrorMessage);
                return RedirectToAction("Index");
            }

            var userResponse = await Mediator.Send(new GetUserByNameQuery { UserName = User.Identity!.Name });

            if (userResponse.Output.Role != WebsiteRoles.WebsiteAdmin
                && userResponse.Output!.UserName != postResponse.Output.AuthorName)
            {
                _notification.Error("You are not authorized");
                return RedirectToAction("Index");
            }

            var createPostDto = new CreatePostDto()
            {
                Id = postResponse.Output.Id,
                Title = postResponse.Output.Title,
                ShortDescription = postResponse.Output.ShortDescription,
                Description = postResponse.Output.Description,
                ThumbnailUrl = postResponse.Output.ThumbnailUrl,
            };

            return View(createPostDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreatePostDto createPostDto)
        {
            if (!ModelState.IsValid) { return View(createPostDto); }
            if (createPostDto.Thumbnail != null)
            {
                createPostDto.ThumbnailUrl = _imageUtility.Upload(createPostDto.Thumbnail);
            }
            var response = await Mediator.Send(new UpdatePostCommand
            {
                PostId = createPostDto.Id,
                ThumbnailUrl = createPostDto.ThumbnailUrl,
                Title = createPostDto.Title,
                ShortDescription = createPostDto.ShortDescription,
                Description = createPostDto.Description,
            });
            if (!response.Conclusion)
            {
                _notification.Error(response.ErrorDescription.ErrorMessage);
                return View();
            }
            _imageUtility.Remove(response.Output.RemoveThumbnailUrl);
            _notification.Success("Post updated succesfully");
            return RedirectToAction("Index", "Post", new { area = "Dashboard" });
        }
    }
}
