using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.AutoMappers;
using Service.ViewModels.ContactMessage;

namespace Service.Service.Interfaces
{
    public interface IContactMessageService
    {
        Task<bool> CreateAsync(ContactMessageCreateVM vm, ModelStateDictionary modelState);
    }
}
