using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;
using Service.ViewModels.Product;

namespace Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {
            var result = await _productService.CreateAsync(vm, ModelState);
            if (!result)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(vm);
            }
            TempData["SuccessMessage"] = "Product successfully added!";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var productDto = await _productService.GetUpdatedDtoAsync(id);

            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(productDto);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductUpdateVM vm)
        {
            var result = await _productService.UpdateAsync(vm, ModelState);
            if (!result)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(vm);
            }

            TempData["SuccessMessage"] = "Category successfully updated!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productService.GetAsync(id);

            return View(product);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
