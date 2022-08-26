using Microsoft.EntityFrameworkCore;
using Validator.Data.Contexto;
using Validator.Domain.Core.Interfaces;

namespace Validator.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;
        private readonly ValidatorContext _context;
        private readonly IUserResolver _userResolver;

        public UnitOfWork(ValidatorContext context, IUserResolver userResolver)
        {
            _context = context;
            _userResolver = userResolver;
        }

        public void Commit()
        {
            ApplyYearValues().GetAwaiter().GetResult();
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await ApplyYearValues();
            await _context.SaveChangesAsync();
        }

        private async Task ApplyYearValues()
        {
            var entries = _context.ChangeTracker.Entries().ToList();
            var entriesWithDivisions = _context.ChangeTracker.Entries().Where(w => w.Entity is IAnoBase);

            foreach (var entry in entriesWithDivisions)
            {
                var entryMustHaveYear = entry.Entity as IAnoBase;

                if (entryMustHaveYear == null) continue;

                if (entry.State == EntityState.Added)
                    entryMustHaveYear.AnoBaseId = await _userResolver.GetYearIdAsync();
                else
                    entry.Property(nameof(IAnoBase.AnoBaseId)).IsModified = false;
            }
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
