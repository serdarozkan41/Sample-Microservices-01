using SM01.Domain.Entities;

namespace SM01.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IUnitOfWork UnitOfWork { get; }

        IQueryable<TEntity> GetAll();

        Task AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        void Delete(TEntity entity);

        Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query, CancellationToken cancellationToken = default);

        Task<T> SingleOrDefaultAsync<T>(IQueryable<T> query, CancellationToken cancellationToken = default);

        Task<List<T>> ToListAsync<T>(IQueryable<T> query, CancellationToken cancellationToken = default);

    }
}
