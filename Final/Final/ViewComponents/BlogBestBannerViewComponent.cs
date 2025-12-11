using Microsoft.AspNetCore.Mvc;

namespace Final.ViewComponents
{
    public class BlogBestBannerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
