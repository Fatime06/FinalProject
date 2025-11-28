using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.DTOs.Category;

namespace Service.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> CreateAsync(CategoryCreateDto dto, ModelStateDictionary modelState);
        Task<bool> UpdateAsync(CategoryUpdateDto dto, ModelStateDictionary modelState);
        Task DeleteAsync(int id);
        Task<CategoryDto> GetAsync(int id);
        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryUpdateDto> GetUpdatedDtoAsync(int id);
    }
}
