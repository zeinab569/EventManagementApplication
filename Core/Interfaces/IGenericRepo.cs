using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepo
    {
        public interface IGenericRepo<T> where T : BaseEntity
        {
            Task<IReadOnlyList<T>> GetListAsync();
            Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
            Task<T> GetByIDAsync(int id);
            Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties);
            Task<T> AddAsync(T entity);
            Task DeleteAsync(int id);
            Task UpdateAsync(int id, T entity);
        }
    }
}
