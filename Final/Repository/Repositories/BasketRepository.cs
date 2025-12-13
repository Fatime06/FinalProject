using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class BasketRepository : Repository<Basket>, IBasketRepository
    {
        private readonly AppDbContext _context;
        public BasketRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Basket?> GetByUserIdAsync(string userId)
        {
            return await _context.Baskets
           .Include(b => b.BasketItems)
           .ThenInclude(bi => bi.Product)
           .FirstOrDefaultAsync(b => b.AppUserId == userId);
        }
    }
}
