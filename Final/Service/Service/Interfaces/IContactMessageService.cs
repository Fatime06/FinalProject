using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.ViewModels.ContactMessage;

namespace Service.Service.Interfaces
{
    public interface IContactMessageService
    {
        Task<bool> CreateAsync(ContactMessageCreateVM vm, ModelStateDictionary modelState);
        Task DeleteAsync(int id);
        Task<ContactMessageVM> GetAsync(int id);
        Task<IEnumerable<ContactMessageVM>> GetAllAsync();
        IQueryable<ContactMessageVM> GetHistoriesQuery();
    }
}
