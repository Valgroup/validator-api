using Validator.Domain.Core.Interfaces;
using Validator.Domain.Entities;

namespace Validator.Domain.Interfaces
{
    public interface ISetorService : IServiceDomain<Setor>
    {
        Task<IEnumerable<Setor>> FindAllByYear();
    }
}
