using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace Final.ViewComponents
{
    public class BestSellersSecondViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public BestSellersSecondViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productService.GetBestSellersAsync(3);
            return View(products);
        }
    }
}
