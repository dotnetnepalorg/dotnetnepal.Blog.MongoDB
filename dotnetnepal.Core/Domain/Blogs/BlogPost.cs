using System;
using System.Collections.Generic;

namespace dotnetnepal.Core.Domain.Blogs
{

    public partial class BlogPost : BaseEntity, ISlugSupported
    {
    
        public string Title { get; set; }

        public string Image { get; set; }

  
        public string Body { get; set; }
              
        public string BodyOverview { get; set; }


        public bool AllowComments { get; set; }


        public int CommentCount { get; set; }


        public string Tags { get; set; }

     
        public DateTime? StartDateUtc { get; set; }

        public DateTime? EndDateUtc { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }


        public DateTime CreatedOnUtc { get; set; }

    }

}
