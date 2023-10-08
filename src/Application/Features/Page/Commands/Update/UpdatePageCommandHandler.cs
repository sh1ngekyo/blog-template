using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogTemplate.Application.Features.Page.Commands.Update
{
    public class UpdatePageCommandHandler 
        : IRequestHandler<UpdatePageCommand, Result<UpdatePageCommandResponse>>
    {
        private readonly IApplicationDbContext _context;
        public UpdatePageCommandHandler(IApplicationDbContext context) =>
            _context = context;

        public async Task<Result<UpdatePageCommandResponse>> Handle(UpdatePageCommand request,
            CancellationToken cancellationToken)
        {
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (page == null)
            {
                return new Result<UpdatePageCommandResponse>(ErrorType.NotFound);
            }
            var response = new UpdatePageCommandResponse();
            page.Title = request.Title;
            page.ShortDescription = request.ShortDescription;
            page.Description = request.Description;
            if (request.ThumbnailUrl != null)
            {
                response.RemoveThumbnailUrl = page.ThumbnailUrl;
                page.ThumbnailUrl = request.ThumbnailUrl;
            }
            _context.Pages!.Update(page);
            await _context.SaveChangesAsync(cancellationToken);
            return new Result<UpdatePageCommandResponse>(ResultType.Updated).SetOutput(response);
        }
    }
}
