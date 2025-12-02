using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;
using Service.ViewModels.History;

namespace Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HistoryController : Controller
    {
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var histories = await _historyService.GetAllAsync();
            return View(histories);
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
        public async Task<IActionResult> Delete(int id)
        {
            await _historyService.DeleteAsync(id);
            TempData["SuccessMessage"] = "History successfully deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
