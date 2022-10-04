using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Domain.Commands.Dashes;
using Validator.Domain.Core;
using Validator.Domain.Dtos.Dashes;

namespace Validator.Application.Interfaces
{
    public interface IDashAppService
    {
        Task<ValidationResult> AdicionarOuAtualizar(ParametroSalvarCommand command);
        Task<DashResultadosDto> ObterResultados(ConsultarResultadoCommand command);
        Task<PermissaoDto> ObterPermissao();
        Task<ParametroDto> ObterParametros();

        Task<ValidationResult> IniciarProcesso(string url);

        Task<ValidationResult> ExcluirProcessoAnoAtual();
    }
}
