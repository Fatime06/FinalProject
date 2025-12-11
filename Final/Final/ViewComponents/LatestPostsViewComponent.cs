using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace Final.ViewComponents
{
    public class LatestPostsViewComponent : ViewComponent
    {
        private readonly IBlogService _blogService;

        public LatestPostsViewComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var latest = await _blogService.GetLatestPostsAsync();
            return View(latest);
        }
    }
}
