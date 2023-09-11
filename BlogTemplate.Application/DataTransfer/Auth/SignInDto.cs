using System.ComponentModel.DataAnnotations;

namespace BlogTemplate.Application.DataTransfer.Auth
{
    public class SignInDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        public bool RememberMe { get; set; } = false;
        public bool IsLockout { get; set; } = true;
    }
}
