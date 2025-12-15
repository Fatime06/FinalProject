using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;
using Service.ViewModels.Order;

namespace Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(
    AuthenticationSchemes = "AdminScheme",
    Roles = "Admin,SuperAdmin"
)]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) page = 1;

            int pageSize = 5;

            var query = _orderService.GetOrdersQuery();

            var model = await PaginatedList<OrderVM>.CreateAsync(
                query,
                page,
                pageSize
            );

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MarkDelivered(int id)
        {
            await _orderService.MarkDeliveredAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var order = await _orderService.GetAsync(id);
            return View(order);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderService.GetAsync(id);
            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(OrderVM vm)
        {
            await _orderService.UpdateStatusAsync(vm.Id, vm.Status);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderService.GetAsync(id);
            return View(order);
        }
        [Authorize(
    AuthenticationSchemes = "AdminScheme",
    Roles = "SuperAdmin"
)]
        [HttpPost]
        public async Task<IActionResult> Delete(OrderVM vm)
        {
            await _orderService.DeleteAsync(vm.Id);
            return RedirectToAction(nameof(Index));
        }
    }

}
