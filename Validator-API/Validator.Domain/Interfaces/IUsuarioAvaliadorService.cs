using System.Linq.Expressions;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Entities;

namespace Validator.Domain.Interfaces
{
    public interface IUsuarioAvaliadorService : IServiceDomain<UsuarioAvaliador>
    {
        Task<UsuarioAvaliador?> Find(Expression<Func<UsuarioAvaliador, bool>> predicate);
        Task<IEnumerable<UsuarioAvaliador>> FindAll(Expression<Func<UsuarioAvaliador, bool>> predicate);
    }
}
