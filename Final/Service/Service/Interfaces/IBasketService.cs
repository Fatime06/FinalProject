using Service.ViewModels.Basket;

namespace Service.Service.Interfaces
{
    public interface IBasketService
    {
        Task AddToBasketAsync(int productId);
        Task<BasketVM> GetBasketAsync();
        Task RemoveItemAsync(int itemId);
    }
}
