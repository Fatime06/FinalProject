using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.DTOs.Category;

namespace Service.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> CreateAsync(CategoryCreateVM dto, ModelStateDictionary modelState);
        Task<bool> UpdateAsync(CategoryUpdateVM dto, ModelStateDictionary modelState);
        Task DeleteAsync(int id);
        Task<CategoryVM> GetAsync(int id);
        Task<IEnumerable<CategoryVM>> GetAllAsync();
        Task<CategoryUpdateVM> GetUpdatedDtoAsync(int id);
    }
}
