using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.ViewModels.Review;

namespace Service.Service.Interfaces
{
    public interface IReviewService
    {
        Task<bool> CreateAsync(ReviewCreateVM vm, ModelStateDictionary modelState);
        Task<bool> UpdateAsync(ReviewUpdateVM vm, ModelStateDictionary modelState);
        Task DeleteAsync(int id);
        Task<ReviewVM> GetAsync(int id);
        Task<IEnumerable<ReviewVM>> GetAllAsync(int take = 5);
        Task<ReviewUpdateVM> GetUpdatedVmAsync(int id);
    }
}
