using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace Final.ViewComponents
{
    public class DealsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public DealsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var deals = await _productService.GetWeeklyDealsAsync();
            return View(deals);
        }
    }
}
