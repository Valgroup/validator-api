using System.Linq.Expressions;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Services;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;

namespace Validator.Domain.Services
{
    public class UsuarioAvaliadorService : ServiceDomain<UsuarioAvaliador>, IUsuarioAvaliadorService
    {
        private readonly IRepository<UsuarioAvaliador> _repository;

        public UsuarioAvaliadorService(IRepository<UsuarioAvaliador> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<UsuarioAvaliador?> Find(Expression<Func<UsuarioAvaliador, bool>> predicate)
        {
            return await _repository.FirstOrDefaultAsync(predicate, false);
        }

        public async Task<IEnumerable<UsuarioAvaliador>> FindAll(Expression<Func<UsuarioAvaliador, bool>> predicate)
        {
            return await _repository.FindAll(predicate);
        }
    }
}
