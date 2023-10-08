using BlogTemplate.Application.Abstractions;
using MediatR;

namespace BlogTemplate.Application.Features.Comments.Queries.GetAllByPostId
{
    public class GetAllCommentsByPostIdQuery : IRequest<Result<List<Domain.Models.Comment>>>
    {
        public int? PostId { get; set; }
    }
}
