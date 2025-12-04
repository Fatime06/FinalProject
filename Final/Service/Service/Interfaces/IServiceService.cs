using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.ViewModels.Brand;
using Service.ViewModels.Service;

namespace Service.Service.Interfaces
{
    public interface IServiceService
    {
        Task<bool> CreateAsync(ServiceCreateVM vm, ModelStateDictionary modelState);
        Task<bool> UpdateAsync(ServiceUpdateVM vm, ModelStateDictionary modelState);
        Task DeleteAsync(int id);
        Task<ServiceVM> GetAsync(int id);
        Task<IEnumerable<ServiceVM>> GetAllAsync();
        Task<ServiceUpdateVM> GetUpdatedVmAsync(int id);
    }
}
