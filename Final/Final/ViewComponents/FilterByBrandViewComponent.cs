using Microsoft.AspNetCore.Mvc;

namespace Final.ViewComponents
{
    public class FilterByBrandViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
