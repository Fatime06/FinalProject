using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.ViewModels.History;

namespace Service.Service.Interfaces
{
    public interface IHistoryService
    {
        Task<bool> CreateAsync(HistoryCreateVM vm, ModelStateDictionary modelState);
        Task<bool> UpdateAsync(HistoryUpdateVM vm, ModelStateDictionary modelState);
        Task DeleteAsync(int id);
        Task<HistoryVM> GetAsync(int id);
        Task<IEnumerable<HistoryVM>> GetAllAsync();
        Task<HistoryUpdateVM> GetUpdatedVmAsync(int id);
    }
}
