using Domain.Entities;
using Repository.Data;

namespace Repository.Repositories
{
    public class HistoryRepository : Repository<History>,Interfaces.IHistoryRepository
    {
        public HistoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
