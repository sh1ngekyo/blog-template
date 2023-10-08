using BlogTemplate.Application.Features.Home.Queries.GetByPageNumber;
using BlogTemplate.Domain.Models;
using BlogTemplate.Presentation.Abstractions.Controller;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogTemplate.Presentation.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<IActionResult> Index(int? page)
        {
            var response = await Mediator.Send(new GetHomeByPageNumberQuery()
            {
                Page = page ?? 1,
                PageSize = 4
            });
            return View(response.Output);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}