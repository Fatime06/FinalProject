using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace Final.ViewComponents
{
    public class FilterByCategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public FilterByCategoryViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? activeCategoryId)
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.ActiveCategory = activeCategoryId;
            return View(categories);
        }
    }
}
