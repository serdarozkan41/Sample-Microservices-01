using SM01.CrossCuttingConcerns.Exceptions;
using SM01.Domain.Entities;
using SM01.Domain.Repositories;

namespace SM01.Application.Common.Services
{
    public class CrudService<T> : ICrudService<T> where T : BaseEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IRepository<T> _repository;

        public CrudService(IRepository<T> repository)
        {
            _unitOfWork = repository.UnitOfWork;
            _repository = repository;
        }

        public Task<List<T>> GetAsync()
        {
            return _repository.ToListAsync(_repository.GetAll());
        }

        public Task<T> GetByIdAsync(Guid Id)
        {
            ValidationException.Requires(Id != Guid.Empty, "Invalid Id");
            return _repository.FirstOrDefaultAsync(_repository.GetAll().Where(x => x.Id == Id));
        }

        public async Task AddOrUpdateAsync(T entity)
        {
            await _repository.AddOrUpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _repository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
