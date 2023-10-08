using AutoMapper;

using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.DataTransfer.Page;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogTemplate.Application.Features.Page.Queries.GetBySlug
{
    public class GetPageBySlugQueryHandler 
        : IRequestHandler<GetPageBySlugQuery, Result<PageDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetPageBySlugQueryHandler(IApplicationDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<Result<PageDto>> Handle(GetPageBySlugQuery request,
            CancellationToken cancellationToken)
        {
            var page = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == request.Slug);
            return page is not null ? 
                new Result<PageDto>(ResultType.Ok).SetOutput(_mapper.Map<PageDto>(page)) 
                : new Result<PageDto>(ErrorType.NotFound);
        }
    }
}
