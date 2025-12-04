using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Category;
using Service.Service.Interfaces;
using Service.ViewModels.Service;

namespace Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var services = await _serviceService.GetAllAsync();
            return View(services);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ServiceCreateVM vm)
        {
            var result = await _serviceService.CreateAsync(vm, ModelState);
            if (!result)
                return View(vm);
            TempData["SuccessMessage"] = "Service successfully added!";

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var service = await _serviceService.GetUpdatedVmAsync(id);
            return View(service);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ServiceUpdateVM vm)
        {
            var result = await _serviceService.UpdateAsync(vm, ModelState);
            if (!result)
                return View(vm);
            TempData["SuccessMessage"] = "Service successfully deleted!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var service = await _serviceService.GetAsync(id);
            return View(service);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Service successfully deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
