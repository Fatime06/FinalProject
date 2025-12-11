using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace Final.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var blogs = await _blogService.GetPaginatedAsync(page, 6);
            return View(blogs);
        }
    }
}
