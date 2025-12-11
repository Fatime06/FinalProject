using Microsoft.AspNetCore.Mvc;

namespace Final.ViewComponents
{
    public class ShopHeaderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
