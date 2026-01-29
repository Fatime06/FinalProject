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
        private readonly ICategoryService _catService;

        public ShopController(IProductService productService, ICategoryService catService)
        {
            _productService = productService;
            _catService = catService;
        }

        public async Task<IActionResult> Index(
    int page = 1,
    int? categoryId = null,
    int? brandId = null,
    decimal? maxPrice = null)
        {
            if (page < 1) page = 1;

            int pageSize = 9;

            var query = _productService.GetProductsAsQueryabe();

            ViewData["MaxPrice"] = await query.MaxAsync(p => p.Price);

            if (categoryId != null)
            {
                query = query.Where(p => p.Category.Id == categoryId);
                ViewData["ActiveCategory"] = categoryId;
                var category = await _catService.GetAsync((int)categoryId);
                ViewData["Category"] = category.Name;
            }

            if (brandId != null)
            {
                query = query.Where(p => p.Brand.Id == brandId);
                ViewBag.ActiveBrand = brandId;
            }


            if (maxPrice != null)
                query = query.Where(p => p.Price <= maxPrice);

            var model = await PaginatedList<ProductVM>.CreateAsync(
                query,
                page,
                pageSize
            );

            return View(model);
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
        [HttpGet]
        public async Task<IActionResult> Search(string search)
        {
            var products = await _productService
                .GetProductsAsQueryabe()
                .Where(p => p.Name.Contains(search))
                .Select(p => new
                {
                    id = p.Id,
                    name = p.Name,
                    price = p.DiscountPrice ?? p.Price,
                    image = p.Image
                })
                .Take(8)
                .ToListAsync();

            return Json(products);
        }
    }
}
