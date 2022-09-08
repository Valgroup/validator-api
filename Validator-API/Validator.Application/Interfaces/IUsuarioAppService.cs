using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Domain.Commands.Usuarios;
using Validator.Domain.Core;

namespace Validator.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        Task<ValidationResult> DeleteAsync(Guid id);
        Task<ValidationResult> EscolherAvaliadores(List<Guid> ids);
        Task<ValidationResult> SubstituirAvaliador(SubstituirAvaliadorCommand command);
    }
}
