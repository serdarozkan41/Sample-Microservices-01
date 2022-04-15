using SM01.Domain.Entities;

namespace SM01.Application.Common.Services
{
    public interface ICrudService<T> where T : BaseEntity
    {
        Task<List<T>> GetAsync(CancellationToken cancellationToken = default);

        Task<T> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default);

        Task AddOrUpdateAsync(T entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    }
}
