using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SM01.Domain.Repositories;
using System.Reflection;

namespace SM01.Infrastructure.Persistence
{
    public class DbContextUnitOfWork<TDbContext> : DbContext, IUnitOfWork where TDbContext : DbContext
    {
        private IDbContextTransaction _dbContextTransaction;

        public DbContextUnitOfWork(IDbContextTransaction dbContextTransaction)
        {
            _dbContextTransaction = dbContextTransaction;
        }

        public DbContextUnitOfWork(DbContextOptions options, IDbContextTransaction dbContextTransaction) : base(options)
        {
            _dbContextTransaction = dbContextTransaction;
        }

        public IDisposable BeginTransaction()
        {
            _dbContextTransaction = Database.BeginTransaction();
            return _dbContextTransaction;
        }

        public async Task<IDisposable> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _dbContextTransaction = await Database.BeginTransactionAsync(cancellationToken);
            return _dbContextTransaction;
        }

        public void CommitTransaction()
        {
            _dbContextTransaction.Commit();
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _dbContextTransaction.CommitAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}