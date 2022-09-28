using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Services;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;

namespace Validator.Domain.Services
{
    public class ProgressoService : ServiceDomain<Progresso>, IProgressoService
    {
        public ProgressoService(IRepository<Progresso> repository) : base(repository)
        {
        }
    }
}
