using System;
using System.Linq;
using System.Threading.Tasks;
namespace BM.DataAccess
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Delete<TEntity>(Guid Id) where TEntity : class;
        void Dispose();
        IQueryable<TEntity> Get<TEntity>() where TEntity : class;
        TEntity GetById<TEntity>(Guid id) where TEntity : class;
        TEntity Find<TEntity>(Nullable<Guid> id) where TEntity : class;
        TEntity Insert<TEntity>(TEntity tEntity) where TEntity : class;
        void SaveChanges();
        void Update<TEntity>(TEntity tEntity) where TEntity : class;

        Task<TEntity> GetByIdAsync<TEntity>(Guid? id) where TEntity : class;

        Task<int> SaveChangesAsync();
    }
}
