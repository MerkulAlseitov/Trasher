using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasher.DAL.Repositories.Interfaces;
using Trasher.Domain.Common;

namespace Trasher.DAL.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();

            if (_dbSet == default(DbSet<T>))
                throw new ArgumentNullException(nameof(DbSet<T>));
        }

        public async Task<T> AddAsync(T item)
        {
            EntityEntry<T> entity = await _dbSet.AddAsync(item);
            T result = entity.Entity;

            await _context.SaveChangesAsync();

            return result;
        }

        public T Add(T item)
        {
            EntityEntry<T> entity = _dbSet.Add(item);
            T result = entity.Entity;

            _context.SaveChanges();

            return result;
        }

        public async Task<List<T>> AddAllAsync(IEnumerable<T> items)
        {
            List<T> result = new List<T>();

            foreach (T item in items)
            {
                EntityEntry<T> entityEntry = await _dbSet.AddAsync(item);
                T entity = entityEntry.Entity;
                result.Add(entity);
            }
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T item = await _dbSet
                .FirstOrDefaultAsync(obj => obj.Id.Equals(id));

            return item == null
                ? throw new ArgumentNullException(nameof(id), $"Entity not found by id {id}")
                : item;
        }

        public async Task Update(T item)
        {
            _dbSet.Update(item);

            _context.SaveChanges();
        }

        public async Task Delete(T item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var item = await _dbSet.FindAsync(id);
            if (item == null)
            {
                throw new Exception("Entity with this id, not found!");
            }
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetQuery()
        {
            return _dbSet.AsQueryable();
        }
    }
}
