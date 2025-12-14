using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.ViewModels.Product;

namespace Service.Service.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateAsync(ProductCreateVM vm, ModelStateDictionary modelState);
        Task<bool> UpdateAsync(ProductUpdateVM vm, ModelStateDictionary modelState);
        Task DeleteAsync(int id);
        Task<ProductVM> GetAsync(int id);
        Task<Product> GetAsyncWithoutMapping(int id);
        Task<IEnumerable<ProductVM>> GetAllAsync();
        Task<ProductUpdateVM> GetUpdatedVmAsync(int id);
        IQueryable<ProductVM> GetProductsAsQueryabe();
        Task<IEnumerable<ProductVM>> GetByTabAsync(string tab);
        Task<IEnumerable<ProductVM>> GetWeeklyDealsAsync();
        Task<IEnumerable<ProductVM>> GetBestSellersAsync(int take = 10);
        Task<IEnumerable<ProductVM>> GetWineRowProductsAsync();
        Task<IEnumerable<ProductVM>> GetCollectionProductsAsync();
        Task<PaginatedList<ProductVM>> GetPaginatedAsync(int page, int pageSize);
        Task<PaginatedList<ProductVM>> GetFilteredAsync(ProductFilterVM filter);
    }
}
