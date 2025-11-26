using Microsoft.AspNetCore.Mvc;

namespace Final.ViewComponents
{
    public class WineViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
