using Microsoft.EntityFrameworkCore;
using SalesApi.Domain.Contracts.Repository.Base;
using SalesApi.Domain.Entities.Base;
using SalesApi.Repository.Context;

namespace SalesApi.Repository.Repositories.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly SalesDbContext _context;
        private bool _disposed;

        public RepositoryBase(SalesDbContext context)
        {
            _context = context;
            _disposed = false;
        }

        public async virtual Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            _context.SaveChanges();
            return entity;
        }

        public async virtual Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Set<T>().AnyAsync(x => x.Id == id);
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async virtual Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }


        #region Disposable Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _disposed) return;

            _disposed = true;
            _context.Dispose();
        }

        #endregion
    }
}
