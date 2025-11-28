using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> IsExistAsync(string name);
    }
}
