using System.Linq.Expressions;

namespace Validator.Domain.Core.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task CreateAsync(TEntity entity);
        Task CreateRangeAsync(List<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(List<TEntity> entities);
        void Delete(TEntity entity);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking);
        Task<TEntity> FindNoFilter(Expression<Func<TEntity, bool>> predicate);
        TEntity? GetById(Guid id);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<TEntity?> GetByCurrentYear();
        Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false);

    }
}
