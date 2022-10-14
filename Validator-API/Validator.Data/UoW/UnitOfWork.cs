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
            ApllyChangesSoftDelete();
            ApplyYearValues().GetAwaiter().GetResult();
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            ApllyChangesSoftDelete();
            await ApplyYearValues();
            await _context.SaveChangesAsync();
        }

        private async Task ApplyYearValues()
        {
            var entriesWithYears = _context.ChangeTracker.Entries().Where(w => w.Entity is IAnoBase);
            var yearId = await _userResolver.GetYearIdAsync();
            foreach (var entry in entriesWithYears)
            {
                var entryMustHaveYear = entry.Entity as IAnoBase;

                if (entryMustHaveYear == null) continue;

                if (entry.State == EntityState.Added && yearId != Guid.Empty)
                    entryMustHaveYear.AnoBaseId = yearId;
                else
                    entry.Property(nameof(IAnoBase.AnoBaseId)).IsModified = false;
            }
        }

        private void ApllyChangesSoftDelete()
        {
            foreach (var entry in _context.ChangeTracker.Entries().Where(w => w.State == EntityState.Deleted && w.Entity is ISoftDelete))
            {
                entry.State = EntityState.Unchanged;
                entry.Property(nameof(ISoftDelete.Deleted)).CurrentValue = true;
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
