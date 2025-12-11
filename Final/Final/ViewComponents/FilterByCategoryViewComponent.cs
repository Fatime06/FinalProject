using Microsoft.AspNetCore.Mvc;

namespace Final.ViewComponents
{
    public class FilterByCategoryViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
