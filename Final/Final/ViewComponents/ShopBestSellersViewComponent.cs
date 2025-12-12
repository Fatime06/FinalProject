using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace Final.ViewComponents
{
    public class ShopBestSellersViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ShopBestSellersViewComponent(IProductService productService)
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
