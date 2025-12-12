using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace Final.ViewComponents
{
    public class FilterByBrandViewComponent : ViewComponent
    {
        private readonly IBrandService _brandService;

        public FilterByBrandViewComponent(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = await _brandService.GetAllAsync();
            return View(brands);
        }
    }
}
