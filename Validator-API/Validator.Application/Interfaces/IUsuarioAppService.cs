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
        Task<ValidationResult> AprovarSubordinado(List<Guid> usuarioIds);
        Task<ValidationResult> AtivarOuDesativar(Guid usuarioId, bool valor);
        Task<ValidationResult> ExcluirAvaliador(ExcluirSugestaoAvaliadoCommand command);
        Task<ValidationResult> AdicionarAvaliador(AdicionarAvaliadorCommand command);
    }
}
