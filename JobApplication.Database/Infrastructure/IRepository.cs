using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JobApplication.Database.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetDefault(Expression<Func<T, bool>> expression);
    }
}
