using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface IBasketRepository : IRepository<Basket>
    {
        Task<Basket?> GetByUserIdAsync(string userId);
    }
}
