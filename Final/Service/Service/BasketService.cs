using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Service.Interfaces;
using Service.ViewModels.Basket;
using Service.ViewModels.BasketItem;
using System.Security.Claims;

namespace Service.Service
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IProductRepository _productRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepo, IProductRepository productRepo, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _basketRepo = basketRepo;
            _productRepo = productRepo;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task AddToBasketAsync(int productId, int quantity)
        {
            var userId = _httpContextAccessor.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                throw new CustomException(401, "User not authenticated");

            var product = await _productRepo.Find(productId).FirstOrDefaultAsync();
            if (product == null)
                throw new CustomException(404, "Product not found");

            var basket = await _basketRepo.GetAll()
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.AppUserId == userId);

            if (basket == null)
            {
                basket = new Basket { AppUserId = userId };
                await _basketRepo.AddAsync(basket);
            }

            var item = basket.BasketItems
                .FirstOrDefault(x => x.ProductId == productId);

            int currentCount = item?.Count ?? 0;

            if (currentCount + quantity > product.Quantity)
                throw new CustomException( 400,$"{product.Name} stock not enough");

            if (item == null)
            {
                basket.BasketItems.Add(new BasketItem
                {
                    ProductId = productId,
                    Count = quantity,
                    Price = product.DiscountPrice ?? product.Price
                });
            }
            else
            {
                item.Count += quantity;
            }

            await _basketRepo.SaveChangesAsync();
        }
        public async Task ClearDbBasketAsync()
        {
            var userId = _httpContextAccessor.HttpContext?
                .User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) return;

            var basket = await _basketRepo.GetAll()
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.AppUserId == userId);

            if (basket == null) return;

            basket.BasketItems.Clear(); 
            await _basketRepo.SaveChangesAsync();
        }



        public async Task<BasketUIVM> GetBasketFromCookieAsync(List<BasketVM> basketDatas)
        {
            if (basketDatas == null || !basketDatas.Any())
                return new BasketUIVM { Items = new() };

            var productIds = basketDatas.Select(b => b.ProductId).ToList();

            var products = await _productRepo.GetAll()
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            var items = new List<BasketItemVM>();

            foreach (var basketItem in basketDatas)
            {
                var product = products.FirstOrDefault(p => p.Id == basketItem.ProductId);
                if (product == null) continue;

                items.Add(new BasketItemVM
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductImage = product.Image,
                    ProductPrice = product.DiscountPrice ?? product.Price,
                    ProductCount = basketItem.ProductCount
                });
            }

            return new BasketUIVM
            {
                Items = items,
                TotalPrice = items.Sum(x => x.ProductPrice * x.ProductCount),
                TotalCount = items.Sum(x => x.ProductCount)
            };
        }


        public async Task RemoveItemAsync(int itemId)
        {
            var item = await _basketRepo.Find(itemId).FirstOrDefaultAsync();
            if (item == null) throw new CustomException(404, "Item not found!");
            _basketRepo.Delete(item);
            await _basketRepo.SaveChangesAsync();
        }
        public async Task MergeCookieBasketToDbAsync(string userId)
        {
            var context = _httpContextAccessor.HttpContext;

            if (!context.Request.Cookies.ContainsKey("basket"))
                return;

            var cookieBasket =
                JsonConvert.DeserializeObject<List<BasketVM>>(
                    context.Request.Cookies["basket"]);

            if (cookieBasket == null || !cookieBasket.Any())
            {
                context.Response.Cookies.Delete("basket");
                return;
            }

            var basket = await _basketRepo.GetAll()
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.AppUserId == userId);

            if (basket == null)
            {
                basket = new Basket { AppUserId = userId };
                await _basketRepo.AddAsync(basket);
            }

            foreach (var cookieItem in cookieBasket)
            {
                var product = await _productRepo.Find(cookieItem.ProductId)
                    .FirstOrDefaultAsync();

                if (product == null || product.Quantity <= 0)
                    continue;

                var dbItem = basket.BasketItems
                    .FirstOrDefault(x => x.ProductId == cookieItem.ProductId);

                int allowed = Math.Min(cookieItem.ProductCount, product.Quantity);

                if (dbItem == null)
                {
                    basket.BasketItems.Add(new BasketItem
                    {
                        ProductId = cookieItem.ProductId,
                        Count = allowed,
                        Price = product.DiscountPrice ?? product.Price
                    });
                }
                else
                {
                    dbItem.Count = Math.Min(
                        dbItem.Count + allowed,
                        product.Quantity
                    );
                }
            }

            await _basketRepo.SaveChangesAsync();

            context.Response.Cookies.Delete("basket");
        }


        public async Task<BasketUIVM> GetBasketAsync()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return await GetBasketFromDbAsync();
            }

            List<BasketVM> basketDatas =
                _httpContextAccessor.HttpContext.Request.Cookies["basket"] != null
                    ? JsonConvert.DeserializeObject<List<BasketVM>>(
                        _httpContextAccessor.HttpContext.Request.Cookies["basket"])
                    : new List<BasketVM>();

            return await GetBasketFromCookieAsync(basketDatas);
        }
        public async Task RemoveFromDbAsync(int productId)
        {
            var userId = _httpContextAccessor.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var basket = await _basketRepo.GetAll()
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.AppUserId == userId);

            if (basket == null) return;

            var item = basket.BasketItems
                .FirstOrDefault(x => x.ProductId == productId);

            if (item != null)
            {
                basket.BasketItems.Remove(item);
                await _basketRepo.SaveChangesAsync();
            }
        }



        public async Task<BasketUIVM> GetBasketFromDbAsync()
        {
            var userId = _httpContextAccessor.HttpContext?
                .User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return new BasketUIVM { Items = new() };

            var basket = await _basketRepo.GetAll()
                .Include(b => b.BasketItems)
                    .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(b => b.AppUserId == userId);

            if (basket == null || !basket.BasketItems.Any())
                return new BasketUIVM { Items = new() };

            var items = basket.BasketItems
                .Where(bi => bi.Product != null)
                .Select(bi => new BasketItemVM
                {
                    ProductId = bi.Product.Id,
                    ProductName = bi.Product.Name,
                    ProductImage = bi.Product.Image,
                    ProductPrice = bi.Product.DiscountPrice ?? bi.Product.Price,
                    ProductCount = bi.Count
                })
                .ToList();

            return new BasketUIVM
            {
                Items = items,
                TotalPrice = items.Sum(x => x.ProductPrice * x.ProductCount),
                TotalCount = items.Sum(x => x.ProductCount)
            };
        }

        public async Task<Basket> GetBasketByUser(string id)
        {
            var basket = await _basketRepo.GetByUserIdAsync(id);
            if (basket == null) throw new CustomException(404, "Basket not found");
            return basket;
        }
        public async Task<int> GetStockAsync(int productId)
        {
            var product = await _productRepo
                .GetAll()
                .Where(p => p.Id == productId)
                .Select(p => p.Quantity)
                .FirstOrDefaultAsync();

            return product; 
        }
    }
}
