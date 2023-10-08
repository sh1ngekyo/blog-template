using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogTemplate.Application.Features.Settings.Commands.Update
{
    public class UpdateSettingsCommandHandler 
        : IRequestHandler<UpdateSettingsCommand, Result<UpdateSettingsCommandResponse>>
    {
        private readonly IApplicationDbContext _context;
        public UpdateSettingsCommandHandler(IApplicationDbContext context) =>
            _context = context;

        public async Task<Result<UpdateSettingsCommandResponse>> Handle(UpdateSettingsCommand request,
            CancellationToken cancellationToken)
        {
            var settings = await _context.Settings!.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (settings == null)
            {
                return new Result<UpdateSettingsCommandResponse>(ErrorType.NotFound);
            }
            var response = new UpdateSettingsCommandResponse();
            settings.SiteName = request.SiteName;
            settings.Title = request.Title;
            settings.ShortDescription = request.ShortDescription;
            if(request.ThumbnailUrl != null)
            {
                response.RemoveThumbnailUrl = settings.ThumbnailUrl;
                settings.ThumbnailUrl = request.ThumbnailUrl;
            }
            settings.FacebookUrl = request.FacebookUrl;
            settings.TwitterUrl = request.TwitterUrl;
            settings.GithubUrl = request.GithubUrl;
            _context.Settings!.Update(settings);
            await _context.SaveChangesAsync(cancellationToken);
            return new Result<UpdateSettingsCommandResponse>(ResultType.Updated).SetOutput(response);
        }
    }
}
