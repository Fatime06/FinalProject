using Microsoft.AspNetCore.Mvc;

namespace Final.ViewComponents
{
    public class ShopBestBannerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
