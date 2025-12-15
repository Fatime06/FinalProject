using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.ViewModels.Product;
using Service.ViewModels.Slider;

namespace Service.Service.Interfaces
{
    public interface ISliderService
    {
        Task<bool> CreateAsync(SliderCreateVM vm, ModelStateDictionary modelState);
        Task<bool> UpdateAsync(SliderUpdateVM vm, ModelStateDictionary modelState);
        Task DeleteAsync(int id);
        Task<SliderVM> GetAsync(int id);
        Task<IEnumerable<SliderVM>> GetAllAsync();
        Task<SliderUpdateVM> GetUpdatedVmAsync(int id);
        IQueryable<SliderVM> GetSlidersQuery();
    }
}
