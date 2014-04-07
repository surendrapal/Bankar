using System;
using System.Linq;
using System.Threading.Tasks;

namespace BM.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext context;

        public Repository()
        {
            this.context = new DbContext();
        }

        public IQueryable<TEntity> Get<TEntity>() where TEntity : class
        {
            return context.Set<TEntity>().AsQueryable<TEntity>();
        }

        public TEntity GetById<TEntity>(Guid id) where TEntity : class
        {
            return context.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(Guid? id) where TEntity : class
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public TEntity Find<TEntity>(Nullable<Guid> id) where TEntity : class
        {
            return context.Set<TEntity>().Find(id);
        }

        public TEntity Insert<TEntity>(TEntity tEntity) where TEntity : class
        {
            tEntity = context.Set<TEntity>().Add(tEntity);
            return tEntity;
        }

        public void Delete<TEntity>(Guid Id) where TEntity : class
        {
            TEntity tEntity = context.Set<TEntity>().Find(Id);
            context.Set<TEntity>().Remove(tEntity);
        }

        public void Update<TEntity>(TEntity tEntity) where TEntity : class
        {
            context.Entry(tEntity).State = System.Data.Entity.EntityState.Modified;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}