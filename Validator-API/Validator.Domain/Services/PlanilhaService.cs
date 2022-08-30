using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Services;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;

namespace Validator.Domain.Services
{
    public class PlanilhaService : ServiceDomain<Planilha>, IPlanilhaService
    {
        private readonly IRepository<Planilha> _repository;

        public PlanilhaService(IRepository<Planilha> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task CreateRangeAsync(List<Planilha> entities)
        {
            await _repository.CreateRangeAsync(entities);
        }
    }
}
