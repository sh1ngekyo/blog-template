using Microsoft.AspNetCore.Identity;

namespace BlogTemplate.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? About { get; set; }
        public string? ThumbnailUrl { get; set; }
        public List<Post>? Posts { get; set; }
    }
}
