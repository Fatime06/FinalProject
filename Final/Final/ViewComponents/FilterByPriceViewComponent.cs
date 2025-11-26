using Microsoft.AspNetCore.Mvc;

namespace Final.ViewComponents
{
    public class FilterByPriceViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
