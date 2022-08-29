using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Domain.Dtos.Usuarios;

namespace Validator.Domain.Interfaces.Repositories
{
    public interface IUsuarioAuthReadOnlyRepository
    {
        Task<UsuarioAuthDto> ObterPorId(Guid id);
    }
}
