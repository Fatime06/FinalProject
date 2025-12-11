using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace Final.ViewComponents
{
    public class CollectionsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public CollectionsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productService.GetCollectionProductsAsync();
            return View(products);
        }
    }
}
