using Microsoft.AspNetCore.Mvc;
using Service.Service;
using Service.Service.Interfaces;
using Service.ViewModels.Blog;
using Service.ViewModels.Brand;
using System.Threading.Tasks;

namespace Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var blogs = await _blogService.GetAllAsync();
            return View(blogs);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BlogCreateVM vm)
        {
            var result = await _blogService.CreateAsync(vm, ModelState);
            if (!result) return View(vm);
            TempData["SuccessMessage"] = "Blog successfully added!";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var brandVm = await _blogService.GetUpdatedVmAsync(id);
            return View(brandVm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BlogUpdateVM vm)
        {
            var result = await _blogService.UpdateAsync(vm, ModelState);
            if (!result) return View(vm);

            TempData["SuccessMessage"] = "Blog successfully updated!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var brand = await _blogService.GetAsync(id);

            return View(brand);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Blog successfully deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
