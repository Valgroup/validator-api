using System.Linq.Expressions;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Entities;

namespace Validator.Domain.Interfaces
{
    public interface IDivisaoService : IServiceDomain<Divisao>
    {
        Task<IEnumerable<Divisao>> FindAllByYear();
    }
}
