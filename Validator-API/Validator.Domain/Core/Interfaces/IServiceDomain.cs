namespace Validator.Domain.Core.Interfaces
{
    public interface IServiceDomain<TEntity> where TEntity : class
    {
        Task<ValidationResult> CreateAsync(TEntity entity);
        ValidationResult Update(TEntity entity);
        ValidationResult Delete(TEntity entity);
        TEntity? GetById(Guid id);
        Task<TEntity?> GetByIdAsync(Guid id);
    }
}
