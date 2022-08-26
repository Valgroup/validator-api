using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Data.Repositories;
using Validator.Domain.Commands.Dashes;
using Validator.Domain.Dtos.Dashes;

namespace Validator.Data.Dapper
{
    public class DashReadOnlyRepository : BaseConnection, IDashReadOnlyRepository
    {
        public async Task<DashResultadosDto> ObterResultados(ConsultarResultadoCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
