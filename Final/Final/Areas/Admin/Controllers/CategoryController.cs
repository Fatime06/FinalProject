using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Category;
using Service.Service.Interfaces;

namespace Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(
    AuthenticationSchemes = "AdminScheme",
    Roles = "Admin,SuperAdmin"
)]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _catService;

        public CategoryController(ICategoryService catService)
        {
            _catService = catService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) page = 1;

            int pageSize = 5;

            var query = _catService.GetCategoriesQuery();

            var model = await PaginatedList<CategoryVM>.CreateAsync(
                query,
                page,
                pageSize
            );

            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
            var result = await _catService.CreateAsync(vm, ModelState);
            if (!result)
                return View(vm);
            TempData["SuccessMessage"] = "Category successfully added!";

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _catService.GetUpdatedVmAsync(id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryUpdateVM vm)
        {
            var result = await _catService.UpdateAsync(vm, ModelState);
            if (!result)
                return View(vm);
            TempData["SuccessMessage"] = "Category successfully deleted!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var category = await _catService.GetAsync(id);
            return View(category);
        }
        [Authorize(
    AuthenticationSchemes = "AdminScheme",
    Roles = "SuperAdmin"
)]
        public async Task<IActionResult> Delete(int id)
        {
            await _catService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Category successfully deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
