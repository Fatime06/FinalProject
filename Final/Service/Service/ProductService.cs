using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.Service.Interfaces;
using Service.ViewModels.Product;

namespace Service.Service
{
    public class ProductService : IProductService
    {
        public Task<bool> CreateAsync(ProductCreateVM dto, ModelStateDictionary modelState)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductVM>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductVM> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductUpdateVM> GetUpdatedDtoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistForNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ProductUpdateVM dto, ModelStateDictionary modelState)
        {
            throw new NotImplementedException();
        }
    }
}
