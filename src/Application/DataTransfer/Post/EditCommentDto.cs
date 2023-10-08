using System.ComponentModel.DataAnnotations;

namespace BlogTemplate.Application.DataTransfer.Post
{
    public class EditCommentDto
    {
        [Required, StringLength(1000, MinimumLength = 1)]
        public string Content { get; init; }
        [Required]
        public int? CommentId { get; init; }
        [Required]
        public int? PostId { get; init; }
    }
}
