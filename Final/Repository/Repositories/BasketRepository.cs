using Domain.Entities;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class BasketRepository : Repository<Basket>, IBasketRepository
    {
        public BasketRepository(AppDbContext context) : base(context)
        {
        }
    }
}
