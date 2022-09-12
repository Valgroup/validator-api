using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Services;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;

namespace Validator.Domain.Services
{
    public class SetorService : ServiceDomain<Setor>, ISetorService
    {
        public SetorService(IRepository<Setor> repository) : base(repository)
        {
        }
    }
}
