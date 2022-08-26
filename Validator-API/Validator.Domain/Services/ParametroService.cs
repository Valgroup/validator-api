using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Services;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;

namespace Validator.Domain.Services
{
    public class ParametroService : ServiceDomain<Parametro>, IParametroService
    {
        public ParametroService(IRepository<Parametro> repository) : base(repository)
        {
        }
    }
}
