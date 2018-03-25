using Microsoft.AspNetCore.Mvc;
using WebApplication.Services;

namespace WebApplication.Blog.MongoDB.Controllers
{
    public class PostController : Controller
    {
        private readonly IBlogService _blogService;

        public PostController(IBlogService blogService)
        {
            _blogService = blogService;
        }


        public async System.Threading.Tasks.Task<IActionResult> IndexAsync()
        {

            var get = await _blogService.GetAllBlogPosts();

            _blogService.InsertBlogPost(new Models.BlogPost { Body = "test" });


            return View();
        }
    }
}
