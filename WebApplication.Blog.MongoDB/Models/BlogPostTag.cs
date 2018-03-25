namespace WebApplication.Blog.MongoDB.Models
{

    public class BlogPostTag : BaseEntity
    {
        public string Name { get; set; }

        public int BlogPostCount { get; set; }
    }

}
