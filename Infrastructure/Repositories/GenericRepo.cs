using Core.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Core.Interfaces.IGenericRepo;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly EventManagementContext _context;

        public GenericRepo(EventManagementContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetListAsync() => await _context.Set<T>().ToListAsync();

        public async Task<T> GetByIDAsync(int id) => await _context.Set<T>()
            .FirstOrDefaultAsync(a => a.Id == id);

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, T entity)
        {

            var existingEntity = await _context.Set<T>().FindAsync(id);
            if (existingEntity == null)
            {
                return;
            }

            entity.Id = existingEntity.Id;
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            /*
            var existingEntity = await _context.Set<T>().FindAsync(id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            */
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.FirstOrDefaultAsync(n => n.Id == id);
        }


    }
}
