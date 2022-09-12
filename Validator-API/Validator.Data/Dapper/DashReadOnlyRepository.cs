using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Data.Repositories;
using Validator.Domain.Commands.Dashes;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Dtos.Dashes;

namespace Validator.Data.Dapper
{
    public class DashReadOnlyRepository : BaseConnection, IDashReadOnlyRepository
    {
        private readonly IUserResolver _userResolver;

        public DashReadOnlyRepository(IUserResolver userResolver)
        {
            _userResolver = userResolver;
        }

        public async Task<ParametroDto> ObterParametros()
        {
            using var cn = CnRead;
            var anoId = await _userResolver.GetYearIdAsync();
            var parametro = await cn.QueryFirstOrDefaultAsync<ParametroDto>(@"SELECT * FROM Parametro
                                                                        WHERE
                                                                        AnoBaseId = @AnoBaseId ", new { AnoBaseId = anoId });
            if (parametro == null)
                return new ParametroDto();

            return parametro;
        }

        public async Task<DashResultadosDto> ObterResultados(ConsultarResultadoCommand command)
        {
            using var cn = CnRead;

            var sbQry = new StringBuilder();
            sbQry.Append(@"SELECT 
                                COUNT(1) QtdStatus,
	                            CASE 
		                            WHEN Status = 0 THEN 'Enviada'
		                            WHEN Status = 1 THEN 'Confirmada'
		                            END StatusNome,
	                               (SELECT COUNT(1) FROM UsuarioAvaliador) Total
                                FROM UsuarioAvaliador UA
                                INNER JOIN Usuarios U ON U.Id = UA.UsuarioId
                                WHERE 1=1 ");

            if (command.DivisaoId.HasValue)
                sbQry.Append(" AND U.DivisaoId = @DivisaoId ");

            if (command.SetorId.HasValue)
                sbQry.Append(" AND U.SetorId = @SetorId ");

            sbQry.Append(" GROUP BY Status ");

            var resultado = await cn.QueryAsync<DashResultadoQueryDto>(sbQry.ToString(), new { command.DivisaoId, command.SetorId });

            var dashResultado = new DashResultadosDto();

            var enviada = resultado.FirstOrDefault(f => f.StatusNome == Domain.Core.Enums.EStatuAvaliador.Enviada.ToString());
            if (enviada != null && enviada.Total > 0)
                dashResultado.SugestaoEnviadas = (enviada.QtdStatus * 100) / enviada.Total;

            var confirmada = resultado.FirstOrDefault(f => f.StatusNome == Domain.Core.Enums.EStatuAvaliador.Confirmada.ToString());
            if (confirmada != null && confirmada.Total > 0)
                dashResultado.AvaliadoresConfirmados = (confirmada.QtdStatus * 100) / confirmada.Total;

            return dashResultado;

        }
    }
}
