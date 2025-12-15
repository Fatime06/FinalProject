using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;
using Service.ViewModels.Order;
using Service.ViewModels.ProductRating;

namespace Final.Controllers
{
    [Authorize(Roles = "Member")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;
        private readonly IProductRatingService _ratingService;

        public OrderController(IOrderService orderService, IBasketService basketService, IProductRatingService ratingService)
        {
            _orderService = orderService;
            _basketService = basketService;
            _ratingService = ratingService;
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
                    TotalPrice = basket.TotalPrice,
                });
            } 

            return RedirectToAction("Success","Order");
        }
        public IActionResult Success()
        {
            return View();
        }
        public async Task<IActionResult> MyOrders()
        {
            var orders = await _orderService.GetUserOrdersAsync();
            return View(orders);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var order = await _orderService.GetUserOrderDetailAsync(id);
            return View(order);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ProductRatingCreateVM vm)
        {
            await _ratingService.CreateAsync(vm,ModelState);
            return RedirectToAction("MyOrders", "Order");
        }
    }

}
