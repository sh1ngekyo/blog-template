using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.DataTransfer.Home;

using MediatR;

using Microsoft.EntityFrameworkCore;

using X.PagedList;

namespace BlogTemplate.Application.Features.Home.Queries.GetByPageNumber
{
    public class GetHomeByPageNumberQueryHandler : IRequestHandler<GetHomeByPageNumberQuery, Result<HomeDto>>
    {
        private readonly IApplicationDbContext _context;
        public GetHomeByPageNumberQueryHandler(IApplicationDbContext context) =>
            _context = context;

        public async Task<Result<HomeDto>> Handle(GetHomeByPageNumberQuery request,
            CancellationToken cancellationToken)
        {
            var posts = await _context.Posts!
                .Include(x => x.ApplicationUser)
            .OrderByDescending(x => x.CreatedDate)
                .ToPagedListAsync(request.Page, request.PageSize);
            var setting = await _context.Settings!.FirstOrDefaultAsync();
            return new Result<HomeDto>(ResultType.Ok).SetOutput(
                new HomeDto()
                {
                    Posts = posts,
                    Title = setting?.Title,
                    ShortDescription = setting?.ShortDescription,
                    ThumbnailUrl = setting?.ThumbnailUrl,
                });
        }
    }
}
