using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;
using Service.ViewModels.Order;

namespace Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllAsync();
            return View(orders);
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

        [HttpPost]
        public async Task<IActionResult> Delete(OrderVM vm)
        {
            await _orderService.DeleteAsync(vm.Id);
            return RedirectToAction(nameof(Index));
        }
    }

}
