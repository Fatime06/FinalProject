using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.ViewModels.Order;

namespace Service.Service.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(CheckoutVM vm,ModelStateDictionary modelState);
    }
}
