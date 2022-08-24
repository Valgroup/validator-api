using Validator.Data.Contexto;
using Validator.Domain.Core.Interfaces;

namespace Validator.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;
        private readonly ValidatorContext _context;

        public UnitOfWork(ValidatorContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
