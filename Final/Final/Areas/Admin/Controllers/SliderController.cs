using Microsoft.AspNetCore.Mvc;
using Service.Service;
using Service.Service.Interfaces;
using Service.ViewModels.Product;
using Service.ViewModels.Slider;
using System.Threading.Tasks;

namespace Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            return View(sliders);
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
            var productDto = await _sliderService.GetUpdatedVmAsync(id);
            return View(productDto);
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
        public async Task<IActionResult> Delete(int id)
        {
            await _sliderService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
