using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogTemplate.Application.DataTransfer.Profile
{
    public class ProfileEditDto
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? About { get; set; }
        public string? ThumbnailUrl { get; set; }
        public IFormFile? Thumbnail { get; set; }
    }
}
