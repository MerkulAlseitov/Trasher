using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trasher.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        public Task<T> AddAsync(T item); // C
        public T Add(T item); // C
        public Task<List<T>> AddAllAsync(IEnumerable<T> items);
        public Task<List<T>> GetAllAsync(); // R
        public Task<T> GetByIdAsync(int id); // R
        public Task Update(T item); // U
        public Task Delete(T item); // D
        public Task DeleteById(int id); // D
        public IQueryable<T> GetQuery();
    }
}
