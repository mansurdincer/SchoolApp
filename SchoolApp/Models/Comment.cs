using System.Reflection.Metadata;

namespace SchoolApp.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
        
    }
}