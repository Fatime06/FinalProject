using Microsoft.AspNetCore.Mvc;

namespace Final.ViewComponents
{
    public class OffersViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
