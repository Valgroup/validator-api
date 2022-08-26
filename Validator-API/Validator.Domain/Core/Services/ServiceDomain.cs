using Validator.Domain.Core.Interfaces;

namespace Validator.Domain.Core.Services
{
    public class ServiceDomain<TEntity> : IServiceDomain<TEntity> where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        protected ValidationResult ValidationResult { get; }

        public ServiceDomain(IRepository<TEntity> repository)
        {
            _repository = repository;
            ValidationResult = new ValidationResult();
        }

        public async Task<ValidationResult> CreateAsync(TEntity entity)
        {
            if (entity is ISelfValidation selfValidation && !selfValidation.IsEntityValid)
                return selfValidation.ValidationResult;

            await _repository.CreateAsync(entity);
            return ValidationResult;
        }

        public ValidationResult Update(TEntity entity)
        {
            if (entity is ISelfValidation selfValidation && !selfValidation.IsEntityValid)
                return selfValidation.ValidationResult;

            _repository.Update(entity);
            return ValidationResult;
        }

        public ValidationResult Delete(TEntity entity)
        {
            if (entity is ISelfValidation selfValidation && !selfValidation.IsEntityValid)
                return selfValidation.ValidationResult;

            _repository.Delete(entity);
            return ValidationResult;
        }

        public TEntity? GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TEntity?> GetByCurrentYear()
        {
            return await _repository.GetByCurrentYear();
        }
    }
}
