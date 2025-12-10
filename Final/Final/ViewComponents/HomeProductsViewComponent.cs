using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace Final.ViewComponents
{
    public class HomeProductsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public HomeProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string tab = "bestseller")
        {
            var products = await _productService.GetByTabAsync(tab);
            return View(products);
        }
    }
}
