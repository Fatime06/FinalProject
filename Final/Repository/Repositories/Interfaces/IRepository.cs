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
        IQueryable<T> Find(int id);
        Task<int> SaveChangesAsync();
        IQueryable<T> GetAll();
    }
}
