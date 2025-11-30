using Domain.Entities;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class ProductRatingRepository : Repository<ProductRating>, IProductRatingRepository
    {
        public ProductRatingRepository(AppDbContext context) : base(context)
        {
        }
    }
}
