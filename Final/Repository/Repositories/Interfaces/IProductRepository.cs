using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<bool> IsExistAsync(string name);
        Task<Product> GetProductByNameAsync(string name);
    }
}
