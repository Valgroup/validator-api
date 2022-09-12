using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Services;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;

namespace Validator.Domain.Services
{
    public class DivisaoService : ServiceDomain<Divisao>, IDivisaoService
    {
        public DivisaoService(IRepository<Divisao> repository) : base(repository)
        {
        }
    }
}
