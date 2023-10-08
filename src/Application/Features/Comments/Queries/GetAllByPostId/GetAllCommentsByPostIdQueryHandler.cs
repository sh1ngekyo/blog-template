using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Abstractions.Enums;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace BlogTemplate.Application.Features.Comments.Queries.GetAllByPostId
{
    public class GetAllCommentsByPostIdQueryHandler 
        : IRequestHandler<GetAllCommentsByPostIdQuery, Result<List<Domain.Models.Comment>>>
    {
        private readonly IApplicationDbContext _context;
        public GetAllCommentsByPostIdQueryHandler(IApplicationDbContext context) =>
            _context = context;

        public async Task<Result<List<Domain.Models.Comment>>> Handle(GetAllCommentsByPostIdQuery request,
            CancellationToken cancellationToken)
        {
            var comments = await _context.Comments
            .Where(x => x.PostId == request.PostId)
            .AsNoTrackingWithIdentityResolution()
            .Include(c => c.Children)
            .Include(c => c.ApplicationUser)
            .ToListAsync();
            return new Result<List<Domain.Models.Comment>>(ResultType.Ok).SetOutput(
                comments.Where(c => c.ParentId == null)
                .AsParallel()
                .ToList());
        }
    }
}
