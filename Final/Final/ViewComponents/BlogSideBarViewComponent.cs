using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace Final.ViewComponents
{
    public class BlogSideBarViewComponent : ViewComponent
    {
        private readonly IBlogService _blogService;

        public BlogSideBarViewComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _blogService.GetBlogCategoriesAsync();
            return View(categories);
        }
    }
}
