using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Services;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;

namespace Validator.Domain.Services
{
    public class PlanilhaService : ServiceDomain<Planilha>, IPlanilhaService
    {
        public PlanilhaService(IRepository<Planilha> repository) : base(repository)
        {
        }
    }
}
