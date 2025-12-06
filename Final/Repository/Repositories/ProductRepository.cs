using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(c => c.Name.ToLower() == name.Trim().ToLower());
        }

        public async Task<bool> IsExistAsync(string name)
        {
            return await _context.Products.AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }
    }
}
