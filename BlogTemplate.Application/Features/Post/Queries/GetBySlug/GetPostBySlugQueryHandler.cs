using AutoMapper;

using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.DataTransfer.Post;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace BlogTemplate.Application.Features.Post.Queries.GetBySlug
{
    public class GetPostBySlugQueryHandler 
        : IRequestHandler<GetPostBySlugQuery, Result<PostDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetPostBySlugQueryHandler(IApplicationDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<Result<PostDto>> Handle(GetPostBySlugQuery request,
            CancellationToken cancellationToken)
        {
            var post = await _context.Posts!.Include(x => x.ApplicationUser)
                .FirstOrDefaultAsync(x => x.Slug == request.Slug);
            return post is not null ?
                new Result<PostDto>(ResultType.Ok).SetOutput(_mapper.Map<PostDto>(post))
                : new Result<PostDto>(ErrorType.NotFound);
        }
    }
}
