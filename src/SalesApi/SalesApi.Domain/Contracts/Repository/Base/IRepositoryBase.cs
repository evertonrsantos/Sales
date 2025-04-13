using SalesApi.Domain.Entities.Base;

namespace SalesApi.Domain.Contracts.Repository.Base
{
    public interface IRepositoryBase<T> : IDisposable where T : EntityBase
    {
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> ExistsAsync(Guid id);
    }
}
