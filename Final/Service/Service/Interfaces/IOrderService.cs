using Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.ViewModels.Order;

namespace Service.Service.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(CheckoutVM vm,ModelStateDictionary modelState);
        Task<List<OrderVM>> GetAllAsync();
        Task MarkDeliveredAsync(int orderId);
        Task<OrderVM> GetAsync(int orderId);
        Task UpdateStatusAsync(int id, OrderStatus status);
        Task DeleteAsync(int id);
        Task<List<OrderVM>> GetUserOrdersAsync();
        Task<OrderVM> GetUserOrderDetailAsync(int orderId);
    }
}
