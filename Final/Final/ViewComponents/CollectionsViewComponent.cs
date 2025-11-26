using Microsoft.AspNetCore.Mvc;

namespace Final.ViewComponents
{
    public class CollectionsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
