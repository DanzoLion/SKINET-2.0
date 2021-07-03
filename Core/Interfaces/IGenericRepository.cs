using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity    // this is constrained so we can only use certain types with this repository // only useable from classes that derive from our base entity
    {
         Task<T> GetByIdAsync(int id);                 // returns a type of T as an integer
         Task<IReadOnlyList<T>> ListAllAsync();     // returns a list of T
         Task<T> GetEntityWithSpec(ISpecification<T> spec);       
         Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
         Task<int> CountAsync(ISpecification<T> spec);
         void Add(T entity);                                                                // tells entity framework to add and track, update and track, delete and track
         void Update(T entity);                                                            // NB Repositories don't save changes -> unitOfWork saves the changes
         void Delete(T entity);
    }
}