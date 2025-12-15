using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;
using Service.ViewModels.History;

namespace Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(
    AuthenticationSchemes = "AdminScheme",
    Roles = "Admin,SuperAdmin"
)]
    public class HistoryController : Controller
    {
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) page = 1;

            int pageSize = 5;

            var query = _historyService.GetHistoriesQuery();

            var model = await PaginatedList<HistoryVM>.CreateAsync(
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
        public async Task<IActionResult> Create(HistoryCreateVM vm)
        {
            var result = await _historyService.CreateAsync(vm, ModelState);
            if (!result)
                return View(vm);
            TempData["SuccessMessage"] = "History successfully added!";

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var history = await _historyService.GetUpdatedVmAsync(id);
            return View(history);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(HistoryUpdateVM vm)
        {
            var result = await _historyService.UpdateAsync(vm, ModelState);
            if (!result)
                return View(vm);
            TempData["SuccessMessage"] = "History successfully updated!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var history = await _historyService.GetAsync(id);
            return View(history);
        }
        [Authorize(
    AuthenticationSchemes = "AdminScheme",
    Roles = "SuperAdmin"
)]
        public async Task<IActionResult> Delete(int id)
        {
            await _historyService.DeleteAsync(id);
            TempData["SuccessMessage"] = "History successfully deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
