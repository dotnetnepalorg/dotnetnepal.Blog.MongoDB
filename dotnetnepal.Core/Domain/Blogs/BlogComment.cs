using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetnepal.Core.Domain.Blogs
{
    public partial class BlogComment : BaseEntity
    {

        public string CustomerId { get; set; }

        public string CommentText { get; set; }

        public string BlogPostTitle { get; set; }

        
        public string BlogPostId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }


    }

}
