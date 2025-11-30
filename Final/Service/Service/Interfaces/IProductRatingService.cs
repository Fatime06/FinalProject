using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.ViewModels.ProductRating;

namespace Service.Service.Interfaces
{
    public interface IProductRatingService
    {
        Task<bool> CreateAsync(ProductRatingCreateVM dto, ModelStateDictionary modelState);
        Task<bool> UpdateAsync(ProductRatingUpdateVM dto, ModelStateDictionary modelState);
        Task DeleteAsync(int id);
        Task<ProductRatingVM> GetAsync(int id);
        Task<IEnumerable<ProductRatingVM>> GetAllAsync();
        Task<ProductRatingUpdateVM> GetUpdatedVmAsync(int id);
    }
}
