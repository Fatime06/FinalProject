using Microsoft.AspNetCore.Mvc;

namespace Final.ViewComponents
{
    public class BestSellersViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
