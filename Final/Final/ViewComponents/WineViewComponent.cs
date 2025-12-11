using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace Final.ViewComponents
{
    public class WineViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public WineViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productService.GetWineRowProductsAsync();
            return View(products);
        }
    }
}
