using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.ViewModels.Product;
using Service.ViewModels.ProductRatingVM;

namespace Service.Service.Interfaces
{
    public interface IProductRatingService
    {
        Task<bool> CreateAsync(ProductRatingCreateVM dto, ModelStateDictionary modelState);
        Task<bool> UpdateAsync(ProductRatingUpdateVM dto, ModelStateDictionary modelState);
        Task DeleteAsync(int id);
        Task<ProductRatingVM> GetAsync(int id);
        Task<IEnumerable<ProductRatingVM>> GetAllAsync();
        Task<bool> IsExistAsync(int id);
        Task<bool> IsExistForNameAsync(string name);
        Task<ProductRatingUpdateVM> GetUpdatedDtoAsync(int id);
    }
}
