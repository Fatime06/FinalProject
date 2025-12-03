using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.ViewModels.Brand;
using Service.ViewModels.Slider;

namespace Service.Service.Interfaces
{
    public interface IBrandService
    {
        Task<bool> CreateAsync(BrandCreateVM vm, ModelStateDictionary modelState);
        Task<bool> UpdateAsync(BrandUpdateVM vm, ModelStateDictionary modelState);
        Task DeleteAsync(int id);
        Task<BrandVM> GetAsync(int id);
        Task<IEnumerable<BrandVM>> GetAllAsync();
        Task<BrandUpdateVM> GetUpdatedVmAsync(int id);
    }
}
