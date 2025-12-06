using Microsoft.AspNetCore.Mvc;
using Service.Service;
using Service.Service.Interfaces;
using Service.ViewModels.Slider;
using Service.ViewModels.Tag;
using System.Threading.Tasks;

namespace Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            var tags = await _tagService.GetAllAsync();
            return View(tags);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TagCreateVM vm)
        {
            var result = await _tagService.CreateAsync(vm, ModelState);
            if (!result) return View(vm);
            TempData["SuccessMessage"] = "Tag successfully added!";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var tagVm = await _tagService.GetUpdatedVmAsync(id);
            return View(tagVm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TagUpdateVM vm)
        {
            var result = await _tagService.UpdateAsync(vm, ModelState);
            if (!result) return View(vm);

            TempData["SuccessMessage"] = "Tag successfully updated!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var slider = await _tagService.GetAsync(id);

            return View(slider);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _tagService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Tag successfully deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
