using System.Linq.Expressions;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Services;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;

namespace Validator.Domain.Services
{
    public class UsuarioService : ServiceDomain<Usuario>, IUsuarioService
    {
        private readonly IRepository<Usuario> _repository;

        public UsuarioService(IRepository<Usuario> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Usuario?> Find(Expression<Func<Usuario, bool>> predicate)
        {
            return await _repository.FirstOrDefaultAsync(predicate, false);
        }
    }
}
