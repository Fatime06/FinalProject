using Microsoft.AspNetCore.Mvc;

namespace Final.ViewComponents
{
    public class HomeShopViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
