using AspNetCoreHero.ToastNotification.Abstractions;

using BlogTemplate.Application.DataTransfer.Post;
using BlogTemplate.Application.Features.Comments.Commands.Create;
using BlogTemplate.Application.Features.Comments.Commands.Delete;
using BlogTemplate.Application.Features.Comments.Commands.Update;
using BlogTemplate.Application.Features.Comments.Queries.GetAllByPostId;
using BlogTemplate.Application.Features.Post.Queries.GetBySlug;
using BlogTemplate.Application.Features.Profile.Queries.GetByName;
using BlogTemplate.Application.Features.User.Queries.GetByName;
using BlogTemplate.Presentation.Abstractions.Controller;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Presentation.Controllers
{
    public class BlogController : BaseController
    {
        public INotyfService _notification { get; }

        public BlogController(INotyfService notification)
        {
            _notification = notification;
        }

        [HttpGet("[controller]/Post/{slug}")]
        public async Task<IActionResult> Post(string slug)
        {
            IActionResult RedirectOnError()
            {
                _notification.Error("Post not found");
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrWhiteSpace(slug))
            {
                return RedirectOnError();
            }
            var response = await Mediator.Send(new GetPostBySlugQuery
            {
                Slug = slug
            });
            if (!response.Conclusion)
            {
                return RedirectOnError();
            }
            response.Output.Comments = (await Mediator.Send(new GetAllCommentsByPostIdQuery
            {
                PostId = response.Output.Id
            })).Output;
            return View(response.Output);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDto commentCDto)
        {
            if (ModelState.IsValid)
            {
                var user = (await Mediator.Send(new GetUserByNameQuery()
                {
                    UserName = User.Identity!.Name
                })).Output;
                var response = await Mediator.Send(new CreateCommentCommand()
                {
                    ApplicationUserId = user.Id,
                    Content = commentCDto.Content,
                    ParentId = commentCDto.ReplyToCommentId,
                    PostId = commentCDto.PostId
                });
                if (response.Conclusion)
                {
                    _notification.Success("Comment was created");
                    return RedirectToAction("Post", new
                    {
                        slug = response.Output.PostSlug
                    });
                }
            }
            _notification.Success("Post not found");
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditComment(EditCommentDto commentEDto)
        {
            if (ModelState.IsValid)
            {
                var response = await Mediator.Send(new UpdateCommentCommand
                {
                    CommentId = commentEDto.CommentId!.Value,
                    Content = commentEDto.Content,
                });
                if (response.Conclusion)
                {
                    _notification.Success("Comment was updated");
                    return RedirectToAction("Post", new
                    {
                        slug = response.Output.PostSlug
                    });
                }
            }
            _notification.Success("Post not found");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var response = await Mediator.Send(new DeleteCommentCommand
            {
                CommentId = commentId,
            });
            if (response.Conclusion)
            {
                _notification.Success("Comment was deleted");
                return RedirectToAction("Post", new
                {
                    slug = response.Output.PostSlug
                });
            }
            _notification.Success("Post not found");
            return RedirectToAction("Index", "Home");
        }


        [HttpGet("[controller]/Profile/{username}/{page?}")]
        public async Task<IActionResult> Profile(string username, int? page)
        {
            IActionResult RedirectOnError()
            {
                _notification.Error("Profile not found");
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                return RedirectOnError();
            }
            var response = await Mediator.Send(new GetProfileByUserNameQuery
            {
                UserName = username,
            });
            if (response.Conclusion)
            {
                return View(response.Output);
            }
            return RedirectOnError();
        }
    }
}
