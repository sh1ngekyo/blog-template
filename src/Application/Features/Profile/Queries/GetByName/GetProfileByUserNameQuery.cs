using BlogTemplate.Application.Abstractions;
using BlogTemplate.Application.DataTransfer.Profile;

using MediatR;

namespace BlogTemplate.Application.Features.Profile.Queries.GetByName
{
    public class GetProfileByUserNameQuery : IRequest<Result<ProfileDto>>
    {
        public string? UserName { get; set; }
        public int? Page { get; set; }
    }
}
