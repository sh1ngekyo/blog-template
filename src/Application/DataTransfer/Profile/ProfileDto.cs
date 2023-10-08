using X.PagedList;

namespace BlogTemplate.Application.DataTransfer.Profile
{
    public class ProfileDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? About { get; set; }
        public string? UserName { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? UserPicUrl { get; set; }
        public string? ThumbnailUrl { get; set; }
        public IPagedList<Domain.Models.Post>? Posts { get; set; }
    }
}
