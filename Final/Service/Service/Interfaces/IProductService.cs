using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.ViewModels.Product;

namespace Service.Service.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateAsync(ProductCreateVM vm, ModelStateDictionary modelState);
        Task<bool> UpdateAsync(ProductUpdateVM vm, ModelStateDictionary modelState);
        Task DeleteAsync(int id);
        Task<ProductVM> GetAsync(int id);
        Task<IEnumerable<ProductVM>> GetAllAsync();
        Task<ProductUpdateVM> GetUpdatedDtoAsync(int id);
    }
}
