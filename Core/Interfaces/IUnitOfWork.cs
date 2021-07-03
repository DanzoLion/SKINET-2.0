using System;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces{

    public interface IUnitOfWork : IDisposable              // IDisposable looks for a dispose method in IUnitOfWork class // and disposes of context when transaction is finished
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();                                           // this returns the number of changes from our database  and saves the changes to our database
    }
}