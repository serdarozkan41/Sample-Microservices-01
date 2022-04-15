using SM01.Domain.Entities;

namespace SM01.Application.Common.Services
{
    public interface ICrudService<T> where T : BaseEntity
    {
        Task<List<T>> GetAsync();

        Task<T> GetByIdAsync(Guid guid);

        Task AddOrUpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
