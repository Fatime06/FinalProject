using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;
using Service.ViewModels.Blog;

namespace Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(
    AuthenticationSchemes = "AdminScheme",
    Roles = "Admin,SuperAdmin"
)]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public BlogController(IBlogService blogService, ICategoryService categoryService, ITagService tagService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _tagService = tagService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) page = 1;

            int pageSize = 5;

            var query = _blogService.GetBlogsQuery();

            var vmQuery = query
               .OrderByDescending(b => b.CreatedDate)
               .Select(b => new BlogVM
               {
                   Id = b.Id,
                   Title = b.Title,
                   Image = b.MainImage,
                   CreatedAt = b.CreatedDate,
                   CommentCount = b.Comments.Count()
               });

            var model = await PaginatedList<BlogVM>.CreateAsync(
                vmQuery,
                page,
                pageSize
            );

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM vm)
        {
            var result = await _blogService.CreateAsync(vm, ModelState);
            if (!result)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();
                return View(vm);
            }
            TempData["SuccessMessage"] = "Blog successfully added!";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var blogVm = await _blogService.GetUpdatedVmAsync(id);
            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();
            return View(blogVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogUpdateVM vm)
        {
            var result = await _blogService.UpdateAsync(vm, ModelState);
            if (!result)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                ViewBag.Tags = await _tagService.GetAllAsync();
                return View(vm);
            }
            TempData["SuccessMessage"] = "Blog successfully updated!";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            var blog = await _blogService.GetAsync(id);

            return View(blog);
        }
        [Authorize(
    AuthenticationSchemes = "AdminScheme",
    Roles = "Admin,SuperAdmin"
)]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Blog successfully deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
