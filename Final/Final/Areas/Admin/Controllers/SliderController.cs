using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;
using Service.ViewModels.Slider;

namespace Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(
    AuthenticationSchemes = "AdminScheme",
    Roles = "Admin,SuperAdmin"
)]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) page = 1;

            int pageSize = 5;

            var query = _sliderService.GetSlidersQuery();

            var model = await PaginatedList<SliderVM>.CreateAsync(
                query,
                page,
                pageSize
            );

            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateVM vm)
        {
            var result = await _sliderService.CreateAsync(vm, ModelState);
            if (!result) return View(vm);
            TempData["SuccessMessage"] = "Slider successfully added!";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var productVm = await _sliderService.GetUpdatedVmAsync(id);
            return View(productVm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SliderUpdateVM vm)
        {
            var result = await _sliderService.UpdateAsync(vm, ModelState);
            if (!result) return View(vm);

            TempData["SuccessMessage"] = "Slider successfully updated!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var slider = await _sliderService.GetAsync(id);

            return View(slider);
        }
        [Authorize(
    AuthenticationSchemes = "AdminScheme",
    Roles = "SuperAdmin"
)]
        public async Task<IActionResult> Delete(int id)
        {
            await _sliderService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Slider successfully deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
