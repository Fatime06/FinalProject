using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Category;
using Service.Service;
using Service.Service.Interfaces;
using Service.ViewModels.Product;
using Service.ViewModels.Service;

namespace Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(
    AuthenticationSchemes = "AdminScheme",
    Roles = "Admin,SuperAdmin"
)]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) page = 1;

            int pageSize = 5;

            var query = _serviceService.GetServicesQuery();

            var model = await PaginatedList<ServiceVM>.CreateAsync(
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
        [Authorize(
    AuthenticationSchemes = "AdminScheme",
    Roles = "SuperAdmin"
)]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Service successfully deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
