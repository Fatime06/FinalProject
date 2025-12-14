using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Exceptions;
using Service.Service.Interfaces;
using Service.ViewModels.Basket;
using Service.ViewModels.BasketItem;

namespace Final.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;

        public BasketController(IBasketService basketService, IProductService productService)
        {
            _basketService = basketService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<BasketVM> basketDatas;

            if (Request.Cookies["basket"] != null)
            {
                basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basketDatas = new List<BasketVM>();
            }

            List<BasketItemVM> basketItems = [];
            foreach (var item in basketDatas)
            {
                var product = await _productService.GetAsync(item.ProductId);
                if (product == null) return NotFound();

                basketItems.Add(new BasketItemVM
                {
                    ProductId = item.ProductId,
                    ProductName = product.Name,
                    ProductPrice = product.Price,
                    ProductCount = item.ProductCount,
                    ProductImage = product.Image,
                    CategoryName = product.Category.Name
                });
            }

            BasketUIVM basket = new BasketUIVM
            {
                Items = basketItems,
                TotalPrice = basketItems.Sum(i => i.ProductPrice * i.ProductCount)
            };
            return View(basket);
        }
        [HttpPost]
        [Route("basket/addtobasket")]
        public async Task<IActionResult> AddToBasket(AddToBasketVM vm)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    await _basketService.AddToBasketAsync(vm.ProductId, vm.Quantity);

                    var basketVm = await _basketService.GetBasketFromDbAsync();
                    return PartialView("_BasketSidebarPartial", basketVm);
                }

                List<BasketVM> basketDatas =
                    Request.Cookies["basket"] != null
                        ? JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"])
                        : new List<BasketVM>();

                var product = await _productService.GetAsync(vm.ProductId);

                var exist = basketDatas.FirstOrDefault(x => x.ProductId == vm.ProductId);
                int currentCount = exist?.ProductCount ?? 0;

                if (currentCount + vm.Quantity > product.Quantity)
                {
                    return BadRequest($"{product.Name} stock not enough");
                }

                if (exist != null)
                    exist.ProductCount += vm.Quantity;
                else
                    basketDatas.Add(new BasketVM
                    {
                        ProductId = vm.ProductId,
                        ProductCount = vm.Quantity
                    });

                Response.Cookies.Append(
                    "basket",
                    JsonConvert.SerializeObject(basketDatas),
                    new CookieOptions
                    {
                        Path = "/",
                        Expires = DateTimeOffset.UtcNow.AddDays(7)
                    });

                var basketUIVm =
                    await _basketService.GetBasketFromCookieAsync(basketDatas);

                return PartialView("_BasketSidebarPartial", basketUIVm);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
        }




        //[HttpPost]
        //public async Task<IActionResult> Remove(int itemId)
        //{
        //    await _basketService.RemoveItemAsync(itemId);
        //    var basket = await _basketService.GetBasketAsync();
        //    return PartialView("_BasketSidebarPartial", basket);
        //}
        //public async Task<IActionResult> GetCartPartial()
        //{
        //    var basket = await _basketService.GetBasketAsync();
        //    return PartialView("_BasketSidebarPartial", basket);
        //}
        [HttpPost]
        [Route("basket/remove/{productId}")]
        public async Task<IActionResult> Remove(int productId)
        {
            if (User.Identity.IsAuthenticated)
            {
                await _basketService.RemoveFromDbAsync(productId);

                var basketVm = await _basketService.GetBasketFromDbAsync();
                return PartialView("_BasketSidebarPartial", basketVm);
            }

            List<BasketVM> basketDatas;

            if (Request.Cookies["basket"] != null)
                basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(
                    Request.Cookies["basket"]);
            else
                basketDatas = new List<BasketVM>();

            var item = basketDatas.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
                basketDatas.Remove(item);

            Response.Cookies.Append(
                "basket",
                JsonConvert.SerializeObject(basketDatas),
                new CookieOptions
                {
                    Path = "/",
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });

            var basketUIVm =
                await _basketService.GetBasketFromCookieAsync(basketDatas);

            return PartialView("_BasketSidebarPartial", basketUIVm);
        }
    }
}
