using System.ComponentModel.DataAnnotations;

namespace BlogTemplate.Application.DataTransfer.Post
{
    public class CreateCommentDto
    {
        public int? ReplyToCommentId { get; init; }

        [Required, StringLength(1000, MinimumLength = 1)]
        public string Content { get; init; }
        [Required]
        public int PostId { get; init; }
    }
}
