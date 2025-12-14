using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;
using Service.ViewModels.Order;

namespace Final.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;

        public OrderController(IOrderService orderService, IBasketService basketService)
        {
            _orderService = orderService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.GetBasketFromDbAsync();
            if (!basket.Items.Any())
                return RedirectToAction("Index", "Shop");

            return View(new CheckoutVM
            {
                Items = basket.Items,
                TotalPrice = basket.TotalPrice
            });
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutVM vm)
        {
            var result = await _orderService.CreateOrderAsync(vm,ModelState);
            if (!result)
            {
                var basket = await _basketService.GetBasketFromDbAsync();
                if (!basket.Items.Any())
                    return RedirectToAction("Index", "Shop");

                return View(new CheckoutVM
                {
                    Items = basket.Items,
                    TotalPrice = basket.TotalPrice
                });
            } 

            return RedirectToAction("Success","Order");
        }
        [Authorize]
        public IActionResult Success()
        {
            return View();
        }
    }

}
