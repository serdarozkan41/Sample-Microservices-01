using Microsoft.EntityFrameworkCore;
using SM01.CrossCuttingConcerns.OS;
using SM01.Domain.Entities;
using SM01.Domain.Repositories;

namespace SM01.Infrastructure.Persistence
{
    public class DbContextRepository<TDbContext, TEntity> : IRepository<TEntity>
           where TEntity : BaseEntity
           where TDbContext : DbContext, IUnitOfWork
    {
        private readonly TDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;

        protected DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _dbContext;
            }
        }

        public DbContextRepository(TDbContext dbContext, IDateTimeProvider dateTimeProvider)
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity.Id.Equals(default))
            {
                entity.CreatedDateTime = _dateTimeProvider.OffsetNow;
                await DbSet.AddAsync(entity, cancellationToken);
            }
            else
            {
                entity.UpdatedDateTime = _dateTimeProvider.OffsetNow;
            }
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public Task<T1> FirstOrDefaultAsync<T1>(IQueryable<T1> query,CancellationToken cancellationToken = default)
        {
            return query.FirstOrDefaultAsync(cancellationToken);
        }

        public Task<T1> SingleOrDefaultAsync<T1>(IQueryable<T1> query, CancellationToken cancellationToken = default)
        {
            return query.SingleOrDefaultAsync(cancellationToken);
        }

        public Task<List<T1>> ToListAsync<T1>(IQueryable<T1> query, CancellationToken cancellationToken = default)
        {
            return query.ToListAsync(cancellationToken);
        }
    }
}
