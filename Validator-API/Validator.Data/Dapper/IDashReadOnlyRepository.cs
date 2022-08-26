using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Domain.Commands.Dashes;
using Validator.Domain.Dtos.Dashes;

namespace Validator.Data.Dapper
{
    public interface IDashReadOnlyRepository
    {
        Task<DashResultadosDto> ObterResultados(ConsultarResultadoCommand command);
    }
}
