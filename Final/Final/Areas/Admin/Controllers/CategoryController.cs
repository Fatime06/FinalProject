using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Category;
using Service.Service.Interfaces;

namespace Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _catService;

        public CategoryController(ICategoryService catService)
        {
            _catService = catService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _catService.GetAllAsync();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM dto)
        {
            var result = await _catService.CreateAsync(dto, ModelState);
            if (!result)
                return View(dto);
            TempData["SuccessMessage"] = "Category successfully added!";

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _catService.GetUpdatedDtoAsync(id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryUpdateVM dto)
        {
            var result = await _catService.UpdateAsync(dto, ModelState);
            if (!result)
                return View(dto);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var category = await _catService.GetAsync(id);
            return View(category);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _catService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
