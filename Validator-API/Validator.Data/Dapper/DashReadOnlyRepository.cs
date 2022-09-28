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
            sbQry.Append($@"SELECT Id FROM Usuarios
                            WHERE
                            AnoBaseId = @AnoBaseId
                            AND Perfil IN (2,4)");

            if (command.DivisaoId.HasValue)
                sbQry.Append(" AND DivisaoId = @DivisaoId ");

            if (command.SetorId.HasValue)
                sbQry.Append(" AND SetorId = @SetorId ");

            var dashResultado = new DashResultadosDto();

            var usuariosIds = await cn.QueryAsync<Guid>(sbQry.ToString(), new { command.DivisaoId, command.SetorId, AnoBaseId = await _userResolver.GetYearIdAsync() });
            if (usuariosIds.Any())
            {
                var ids = usuariosIds.Select(s => $"'{s}'");
                var whereInUsuario = $" UsuarioId IN ({string.Join(',', ids)}) ";
                var totalUsarios = usuariosIds.Count();
                var qtdEnviadas = await cn.QueryFirstOrDefaultAsync<int>($"SELECT COUNT(1) Qtd FROM Progresso WHERE {whereInUsuario} AND Status = 2");
                var qtdConfirmadas = await cn.QueryFirstOrDefaultAsync<int>($"SELECT COUNT(1) Qtd FROM Progresso WHERE {whereInUsuario} AND Status = 1");

                dashResultado.SugestaoEnviadas = (qtdEnviadas * 100) / totalUsarios;
                dashResultado.AvaliadoresConfirmados = (qtdConfirmadas * 100) / totalUsarios;
            }


            return dashResultado;

        }
    }
}
