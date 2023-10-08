using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.Page;
using MediatR;

namespace BlogTemplate.Application.Features.Page.Queries.GetBySlug
{
    public class GetPageBySlugQuery : IRequest<Result<PageDto>>
    {
        public string? Slug { get; set; }
    }
}
