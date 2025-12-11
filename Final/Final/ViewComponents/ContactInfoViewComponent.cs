using Microsoft.AspNetCore.Mvc;

namespace Final.ViewComponents
{
    public class ContactInfoViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
