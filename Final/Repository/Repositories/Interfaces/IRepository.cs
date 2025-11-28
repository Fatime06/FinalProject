using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Repository.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> FindAsync(int id);
        Task<int> SaveChangesAsync();
        Task<IEnumerable<T>> GetAll();
    }
}
