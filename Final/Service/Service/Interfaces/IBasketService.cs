using Domain.Entities;
using Service.ViewModels.Basket;
using System.Security.Claims;

namespace Service.Service.Interfaces
{
    public interface IBasketService
    {
        Task AddToBasketAsync(int productId, int quantity);
        Task<BasketUIVM> GetBasketFromCookieAsync(List<BasketVM> basketDatas);
        Task RemoveItemAsync(int itemId);
        Task MergeCookieBasketToDbAsync(string userId);
        Task<Basket> GetBasketByUser(string id);
        Task<BasketUIVM> GetBasketFromDbAsync();
        Task<BasketUIVM> GetBasketAsync();
        Task RemoveFromDbAsync(int productId);
        Task ClearDbBasketAsync();
    }
}
