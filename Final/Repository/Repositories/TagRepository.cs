using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        private readonly AppDbContext _context;
        public TagRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Tag> GetCategoryByNameAsync(string name)
        {
            return await _context.Tags.FirstOrDefaultAsync(c => c.Name.ToLower() == name.Trim().ToLower());
        }

        public async Task<bool> IsExistAsync(string name)
        {
            return await _context.Tags.AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }
    }
}
