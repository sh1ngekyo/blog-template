using System.ComponentModel.DataAnnotations;

namespace BlogTemplate.Application.DataTransfer.Profile
{
    public class ResetPasswordDto
    {
        public string? UserName { get; set; }
        [Required]
        public string? NewPassword { get; set; }
        [Compare(nameof(NewPassword))]
        [Required]
        public string? ConfirmPasswor { get; set; }
    }
}
