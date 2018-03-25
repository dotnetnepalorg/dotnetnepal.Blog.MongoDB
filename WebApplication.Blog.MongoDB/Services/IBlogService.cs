using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Blog.MongoDB.Models;

namespace WebApplication.Services
{
    public interface IBlogService
    {
        void DeleteBlogComment(BlogComment blogComment);
        void DeleteBlogPost(BlogPost blogPost);
        Task<IList<BlogPost>> GetAllBlogPosts();
        IList<BlogComment> GetAllComments(string userId);
        BlogComment GetBlogCommentById(string blogCommentId);
        IList<BlogComment> GetBlogCommentsByBlogPostId(string blogPostId);
        IList<BlogComment> GetBlogCommentsByIds(string[] commentIds);
        BlogPost GetBlogPostById(string blogPostId);
        void InsertBlogComment(BlogComment blogComment);
        void InsertBlogPost(BlogPost blogPost);
        void UpdateBlogPost(BlogPost blogPost);
    }
}