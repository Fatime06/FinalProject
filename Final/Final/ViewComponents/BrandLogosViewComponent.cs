using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace Final.ViewComponents
{
    public class BrandLogosViewComponent : ViewComponent
    {
        private readonly IBrandService _brandService;

        public BrandLogosViewComponent(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brandLogos = await _brandService.GetAllAsync();
            return View(brandLogos);
        }
    }
}
