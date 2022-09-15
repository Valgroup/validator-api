using System.Linq.Expressions;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Services;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;

namespace Validator.Domain.Services
{
    public class DivisaoService : ServiceDomain<Divisao>, IDivisaoService
    {
        private readonly IRepository<Divisao> _repository;

        public DivisaoService(IRepository<Divisao> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Divisao>> FindAllByYear()
        {
            return await _repository.FindAllByYear();
        }
    }
}
