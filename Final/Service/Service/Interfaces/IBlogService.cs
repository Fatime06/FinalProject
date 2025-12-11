using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.ViewModels.Blog;
using Service.ViewModels.Product;

namespace Service.Service.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogVM>> GetAllAsync();
        Task<BlogDetailVM> GetAsync(int id);
        Task<bool> CreateAsync(BlogCreateVM vm, ModelStateDictionary modelState);
        Task<bool> UpdateAsync(BlogUpdateVM vm, ModelStateDictionary modelState);
        Task DeleteAsync(int id);
        Task<BlogUpdateVM> GetUpdatedVmAsync(int id);
        Task<PaginatedList<BlogVM>> GetPaginatedAsync(int page, int pageSize);
        Task<IEnumerable<BlogCategoryVM>> GetBlogCategoriesAsync();
        Task<IEnumerable<BlogVM>> GetLatestPostsAsync();
    }
}
