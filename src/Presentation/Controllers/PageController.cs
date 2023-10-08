using BlogTemplate.Application.Features.Page.Queries.GetBySlug;
using BlogTemplate.Presentation.Abstractions.Controller;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Presentation.Controllers
{
    public class PageController : BaseController
    {
        [HttpGet("About")]
        public async Task<IActionResult> About()
        {
            var response = await Mediator.Send(new GetPageBySlugQuery() 
            { 
                Slug = "about" 
            });
            return View(response.Output);
        }

        [HttpGet("Contact")]
        public async Task<IActionResult> Contact()
        {
            var response = await Mediator.Send(new GetPageBySlugQuery()
            {
                Slug = "contact"
            });
            return View(response.Output);
        }

        [HttpGet("PrivacyPolicy")]
        public async Task<IActionResult> PrivacyPolicy()
        {
            var response = await Mediator.Send(new GetPageBySlugQuery()
            {
                Slug = "privacy"
            });
            return View(response.Output);
        }
    }
}
