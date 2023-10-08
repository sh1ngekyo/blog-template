using X.PagedList;

namespace BlogTemplate.Application.DataTransfer.Home
{
    public class HomeDto
    {
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? ThumbnailUrl { get; set; }
        public IPagedList<Domain.Models.Post>? Posts { get; set; }
    }
}
