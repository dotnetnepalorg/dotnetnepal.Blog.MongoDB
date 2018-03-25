using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetnepal.Core.Domain.Blogs
{
    public partial class BlogComment : BaseEntity
    {

        public string UserId { get; set; }
        public string CommentText { get; set; }

        public string BlogPostTitle { get; set; }
                
        public string BlogPostId { get; set; }
        
        public DateTime CreatedOnUtc { get; set; }


    }

}
