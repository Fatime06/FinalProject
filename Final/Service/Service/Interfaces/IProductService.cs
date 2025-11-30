using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.ViewModels.Product;

namespace Service.Service.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateAsync(ProductCreateVM dto, ModelStateDictionary modelState);
        Task<bool> UpdateAsync(ProductUpdateVM dto, ModelStateDictionary modelState);
        Task DeleteAsync(int id);
        Task<ProductVM> GetAsync(int id);
        Task<IEnumerable<ProductVM>> GetAllAsync();
        Task<bool> IsExistAsync(int id);
        Task<bool> IsExistForNameAsync(string name);
        Task<ProductUpdateVM> GetUpdatedDtoAsync(int id);
    }
}
