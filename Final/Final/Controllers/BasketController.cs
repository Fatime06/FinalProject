using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace Final.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _basketService.GetBasketAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Add(int id)
        {
            await _basketService.AddToBasketAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(int id)
        {
            await _basketService.RemoveItemAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> GetCartPartial()
        {
            var basket = await _basketService.GetBasketAsync();
            return PartialView("_CartSidebarPartial", basket);
        }
    }
}
