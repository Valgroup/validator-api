using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Validator.Data.Contexto;
using Validator.Domain.Core.Interfaces;

namespace Validator.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ValidatorContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ValidatorContext db)
        {
            Db = db;
            DbSet = Db.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public Task<TEntity> FindNoFilter(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking)
        {
            throw new NotImplementedException();
        }

        public TEntity? GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }


    }
}
