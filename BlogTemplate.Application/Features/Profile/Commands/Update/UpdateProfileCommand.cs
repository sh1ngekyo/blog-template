using BlogTemplate.Application.Abstractions;

using MediatR;

namespace BlogTemplate.Application.Features.Profile.Commands.Update
{
    public class UpdateProfileCommand : IRequest<Result<UpdateProfileCommandResponse>>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? About { get; set; }
        public string? ThumbnailUrl { get; set; }
    }
}
