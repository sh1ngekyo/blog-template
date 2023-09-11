using AutoMapper;

using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.Abstractions.Database;
using BlogTemplate.Application.Abstractions.Enums;
using BlogTemplate.Application.Common.Exceptions;
using BlogTemplate.Application.DataTransfer.Page;
using BlogTemplate.Application.DataTransfer.Post;
using BlogTemplate.Domain.Models;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Application.Features.Post.Queries.GetById
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, Result<PostDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetPostByIdQueryHandler(IApplicationDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<Result<PostDto>> Handle(GetPostByIdQuery request,
            CancellationToken cancellationToken)
        {
            var post = await _context.Posts!.Include(x => x.ApplicationUser)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            return post is not null ?
                new Result<PostDto>(ResultType.Ok).SetOutput(_mapper.Map<PostDto>(post))
                : new Result<PostDto>(ErrorType.NotFound);
        }
    }
}
