using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Service.Interfaces;
using Service.ViewModels.Product;

namespace Final.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;

        public ShopController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int? categoryId, int page = 1)
        {
            int pageSize = 9;

            var query = _productService.GetProductsAsQueryabe();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.Category.Id == categoryId);
            }

            var products = await PaginatedList<ProductVM>
                .CreateAsync(query, page, pageSize);

            ViewBag.ActiveCategory = categoryId;

            return View(products);
        }


        [HttpGet]
        public async Task<IActionResult> Filter(ProductFilterVM filter)
        {
            var result = await _productService.GetFilteredAsync(filter);
            return PartialView("_ProductGridPartial", result);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productService.GetAsync(id);

            return View(product);
        }
    }
}
