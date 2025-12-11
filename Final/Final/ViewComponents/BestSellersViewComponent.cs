using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace Final.ViewComponents
{
    public class BestSellersViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public BestSellersViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productService.GetBestSellersAsync();
            return View(products);
        }
    }
}
