using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.DTOs.Category;
using Service.ViewModels.Tag;

namespace Service.Service.Interfaces
{
    public interface ITagService
    {
        Task<bool> CreateAsync(TagCreateVM vm, ModelStateDictionary modelState);
        Task<bool> UpdateAsync(TagUpdateVM vm, ModelStateDictionary modelState);
        Task DeleteAsync(int id);
        Task<TagVM> GetAsync(int id);
        Task<IEnumerable<TagVM>> GetAllAsync();
        Task<TagUpdateVM> GetUpdatedVmAsync(int id);
    }
}
