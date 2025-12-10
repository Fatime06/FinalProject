using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Service.Interfaces;
using Service.ViewModels.Basket;
using Service.ViewModels.BasketItem;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

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

        public async Task AddToBasketAsync(int productId)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) throw new CustomException(401, "You must be logged in!");

            var basket = await _basketRepo.GetAll()
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.AppUserId == userId);

            if (basket == null)
            {
                basket = new Basket { AppUserId = userId };
                await _basketRepo.AddAsync(basket);
            }

            var product = await _productRepo.Find(productId).FirstOrDefaultAsync();
            if (product == null) throw new CustomException(404, "Product not found!");

            var item = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);

            if (item == null)
            {
                basket.BasketItems.Add(new BasketItem
                {
                    ProductId = productId,
                    Count = 1,
                    Price = product.Price
                });
            }
            else item.Count++;

            await _basketRepo.SaveChangesAsync();
        }

        public async Task<BasketVM> GetBasketAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var basket = await _basketRepo.GetAll()
                .Include(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(b => b.AppUserId == userId);

            if (basket == null) return new BasketVM { Items = new() };

            var items = _mapper.Map<List<BasketItemVM>>(basket.BasketItems);

            return new BasketVM
            {
                Items = items,
                TotalPrice = items.Sum(x => x.Price * x.Count)
            };
        }

        public async Task RemoveItemAsync(int itemId)
        {
            var item = await _basketRepo.Find(itemId).FirstOrDefaultAsync();
            if (item == null) throw new CustomException(404, "Item not found!");
            _basketRepo.Delete(item);
            await _basketRepo.SaveChangesAsync();
        }
    }
}
