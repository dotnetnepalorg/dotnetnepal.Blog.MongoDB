using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Blog.MongoDB.Models;
using WebApplication.Blog.MongoDB.Repository;

namespace WebApplication.Services
{

    public class BlogService : IBlogService
    {
        #region Fields

        private readonly IRepository<BlogPost> _blogPostRepository;
        private readonly IRepository<BlogComment> _blogCommentRepository;

        #endregion

        #region Ctor

        public BlogService(IRepository<BlogPost> blogPostRepository,
            IRepository<BlogComment> blogCommentRepository
        )
        {
            this._blogPostRepository = blogPostRepository;
            this._blogCommentRepository = blogCommentRepository;
        }

        #endregion

        #region Methods

        public virtual void DeleteBlogPost(BlogPost blogPost)
        {
            if (blogPost == null)
                throw new ArgumentNullException(nameof(blogPost));

            _blogPostRepository.Delete(blogPost);

        }

        public virtual BlogPost GetBlogPostById(string blogPostId)
        {
            return _blogPostRepository.GetById(blogPostId);
        }


        public async Task<IList<BlogPost>> GetAllBlogPosts()
        {
            return await _blogPostRepository.Table.ToListAsync();
        }


        //public async Task<IList<BlogPost>> GetAllBlogPostsByTag(string tag = "")
        //{
        //    tag = tag.Trim();

        //    //we load all records and only then filter them by tag
        //    var blogPostsAll = await GetAllBlogPosts();
        //    var taggedBlogPosts = new List<BlogPost>();
        //    foreach (var blogPost in blogPostsAll)
        //    {
        //        var tags = blogPost.ParseTags();
        //        if (!String.IsNullOrEmpty(tags.FirstOrDefault(t => t.Equals(tag, StringComparison.OrdinalIgnoreCase))))
        //            taggedBlogPosts.Add(blogPost);
        //    }

        //    //server-side paging
        //    return taggedBlogPosts;
        //}


        //public async Task<IList<BlogPostTag>> GetAllBlogPostTags()
        //{
        //    var blogPostTags = new List<BlogPostTag>();

        //    var blogPosts = await GetAllBlogPosts();

        //    foreach (var blogPost in blogPosts)
        //    {
        //        var tags = blogPost.ParseTags();
        //        foreach (string tag in tags)
        //        {
        //            var foundBlogPostTag = blogPostTags.Find(bpt => bpt.Name.Equals(tag, StringComparison.OrdinalIgnoreCase));
        //            if (foundBlogPostTag == null)
        //            {
        //                foundBlogPostTag = new BlogPostTag
        //                {
        //                    Name = tag,
        //                    BlogPostCount = 1
        //                };
        //                blogPostTags.Add(foundBlogPostTag);
        //            }
        //            else
        //                foundBlogPostTag.BlogPostCount++;
        //        }
        //    }

        //    return blogPostTags;
        //}

        public virtual void InsertBlogPost(BlogPost blogPost)
        {
            if (blogPost == null)
                throw new ArgumentNullException(nameof(blogPost));

            _blogPostRepository.Insert(blogPost);


        }


        public virtual void InsertBlogComment(BlogComment blogComment)
        {
            if (blogComment == null)
                throw new ArgumentNullException(nameof(blogComment));

            _blogCommentRepository.Insert(blogComment);


        }


        public virtual void UpdateBlogPost(BlogPost blogPost)
        {
            if (blogPost == null)
                throw new ArgumentNullException(nameof(blogPost));

            _blogPostRepository.Update(blogPost);

        }

        public virtual IList<BlogComment> GetAllComments(string userId)
        {
            var query = from c in _blogCommentRepository.Table
                        orderby c.CreatedOnUtc
                        // where (customerId == "" || c.CustomerId == customerId)
                        select c;
            var content = query.ToList();
            return content;
        }


        public virtual BlogComment GetBlogCommentById(string blogCommentId)
        {
            return _blogCommentRepository.GetById(blogCommentId);
        }

        public virtual IList<BlogComment> GetBlogCommentsByBlogPostId(string blogPostId)
        {
            var query = from c in _blogCommentRepository.Table
                        where c.BlogPostId == blogPostId
                        orderby c.CreatedOnUtc
                        select c;
            var content = query.ToList();
            return content;

        }


        public virtual IList<BlogComment> GetBlogCommentsByIds(string[] commentIds)
        {
            if (commentIds == null || commentIds.Length == 0)
                return new List<BlogComment>();

            var query = from bc in _blogCommentRepository.Table
                        where commentIds.Contains(bc.Id)
                        select bc;
            var comments = query.ToList();
            //sort by passed identifiers
            var sortedComments = new List<BlogComment>();
            foreach (string id in commentIds)
            {
                var comment = comments.Find(x => x.Id == id);
                if (comment != null)
                    sortedComments.Add(comment);
            }
            return sortedComments;
        }

        public virtual void DeleteBlogComment(BlogComment blogComment)
        {
            if (blogComment == null)
                throw new ArgumentNullException(nameof(blogComment));

            _blogCommentRepository.Delete(blogComment);
        }
        #endregion
    }
}
