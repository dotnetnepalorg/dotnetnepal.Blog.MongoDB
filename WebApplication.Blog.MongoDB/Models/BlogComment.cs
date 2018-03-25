using System;

namespace WebApplication.Blog.MongoDB.Models
{
    public class BlogComment : BaseEntity
    {

        public string UserId { get; set; }
        public string CommentText { get; set; }

        public string BlogPostTitle { get; set; }

        public string BlogPostId { get; set; }

        public DateTime CreatedOnUtc { get; set; }


    }

}
