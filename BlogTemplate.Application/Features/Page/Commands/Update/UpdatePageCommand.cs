using BlogTemplate.Application.Abstractions;
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Application.Features.Page.Commands.Update
{
    public class UpdatePageCommand : IRequest<Result<UpdatePageCommandResponse>>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
    }
}
