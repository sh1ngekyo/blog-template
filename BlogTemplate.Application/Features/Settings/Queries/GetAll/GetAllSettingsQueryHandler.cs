using AutoMapper;
using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Abstractions;
using MediatR;
using BlogTemplate.Application.DataTransfer.Settings;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace BlogTemplate.Application.Features.Settings.Queries.GetAll
{
    public class GetAllSettingsQueryHandler 
        : IRequestHandler<GetAllSettingsQuery, Result<List<SettingsDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllSettingsQueryHandler(IApplicationDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<Result<List<SettingsDto>>> Handle(GetAllSettingsQuery request,
            CancellationToken cancellationToken)
        {
            var settings = await _context.Settings!
                .ProjectTo<SettingsDto>(_mapper.ConfigurationProvider)!
                .ToListAsync();
            return new Result<List<SettingsDto>>(ResultType.Ok).SetOutput(settings);
        }
    }
}
