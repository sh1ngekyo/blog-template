using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.Home;
using MediatR;

namespace BlogTemplate.Application.Features.Home.Queries.GetByPageNumber
{
    public class GetHomeByPageNumberQuery : IRequest<Result<HomeDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
