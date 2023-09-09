using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Domain.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int? ParentId { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public Comment? Parent { get; set; }
        public ICollection<Comment>? Children { get; set; }
    }
}
