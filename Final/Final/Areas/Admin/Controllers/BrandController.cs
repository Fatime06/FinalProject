using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;
using Service.ViewModels.Brand;

namespace Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllAsync();
            return View(brands);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BrandCreateVM vm)
        {
            var result = await _brandService.CreateAsync(vm, ModelState);
            if (!result) return View(vm);
            TempData["SuccessMessage"] = "Brand successfully added!";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var brandVm = await _brandService.GetUpdatedVmAsync(id);
            return View(brandVm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BrandUpdateVM vm)
        {
            var result = await _brandService.UpdateAsync(vm, ModelState);
            if (!result) return View(vm);

            TempData["SuccessMessage"] = "Brand successfully updated!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var brand = await _brandService.GetAsync(id);

            return View(brand);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _brandService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Brand successfully deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
