using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.Home;
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Application.Features.Home.Queries.GetByPageNumber
{
    public class GetHomeByPageNumberQuery : IRequest<Result<HomeDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
