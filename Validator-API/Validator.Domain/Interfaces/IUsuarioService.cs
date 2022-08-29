using System.Linq.Expressions;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Entities;

namespace Validator.Domain.Interfaces
{
    public interface IUsuarioService : IServiceDomain<Usuario>
    {
        Task<Usuario?> Find(Expression<Func<Usuario, bool>> predicate);
    }
}
