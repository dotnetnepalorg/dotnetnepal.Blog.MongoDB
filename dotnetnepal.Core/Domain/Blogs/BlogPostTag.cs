namespace dotnetnepal.Core.Domain.Blogs
{

    public partial class BlogPostTag : BaseEntity
    {
        public string Name { get; set; }

        public int BlogPostCount { get; set; }
    }

}
