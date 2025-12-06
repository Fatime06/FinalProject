using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<bool> IsExistAsync(string name);
        Task<Tag> GetCategoryByNameAsync(string name);
    }
}
