namespace Validator.Domain.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
        void Commit();
    }
}
