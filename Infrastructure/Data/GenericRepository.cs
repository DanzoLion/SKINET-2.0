using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data {
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity {
        private readonly StoreContext _context;
        public GenericRepository (StoreContext context) {
            _context = context;
        }

        public async Task<T> GetByIdAsync (int id) {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync () {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
           return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec); // product is converted into a Queryable
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);                      // tracked and implemented as a synchronous method
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);                // attaches entity to be changed
            _context.Entry(entity).State = EntityState.Modified;        // marks the item as modified and adds to list of items to be save in unitofwork
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}