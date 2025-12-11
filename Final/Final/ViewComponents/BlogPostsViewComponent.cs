using Microsoft.AspNetCore.Mvc;

namespace Final.ViewComponents
{
    public class BlogPostsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
