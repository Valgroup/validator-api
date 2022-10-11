using Dapper;
using Validator.Data.Repositories;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Dtos;
using Validator.Domain.Interfaces.Repositories;

namespace Validator.Data.Dapper
{
    public class UtilReadOnlyRepository : BaseConnection, IUtilReadOnlyRepository
    {
        private readonly IUserResolver _userResolver;

        public UtilReadOnlyRepository(IUserResolver userResolver)
        {
            _userResolver = userResolver;
        }

        public async Task<int> ObterQtdPendetes()
        {
            using var cn = CnRead;
            return await cn.QueryFirstOrDefaultAsync<int>(@"SELECT COUNT(1) QTD FROM Planilhas WHERE EhValido = 0");
        }

        public async Task<int> ObterQtdTotal()
        {
            using var cn = CnRead;
            return await cn.QueryFirstOrDefaultAsync<int>(@"SELECT COUNT(1) QTD FROM Planilhas WHERE EhValido = 1");
        }

        public async Task<IEnumerable<SelectedItemDto>> ObterTodasDivisoes()
        {
            using var cn = CnRead;
            return await cn.QueryAsync<SelectedItemDto>(@"SELECT DISTINCT
	                                                            U.DivisaoId AS Id,
	                                                            D.Nome AS Text
                                                           FROM Usuarios U
                                                           INNER JOIN Divisao D ON D.Id = U.DivisaoId
                                                           ORDER BY D.Nome ");
        }

        public async Task<IEnumerable<SelectedItemDto>> ObterTodosSetores()
        {
            using var cn = CnRead;
            return await cn.QueryAsync<SelectedItemDto>(@"SELECT DISTINCT
	                                                            U.SetorId AS Id,
	                                                            S.Nome AS Text
                                                          FROM Usuarios U
                                                          INNER JOIN Setor S ON S.Id = U.SetorId
                                                          ORDER BY S.Nome ");
        }

        public async Task<bool> TemDadosExportacao()
        {
            using var cn = CnRead;
            var dados = await cn.QueryAsync<Guid>("SELECT TOP 1 UsuarioId FROM UsuarioAvaliador WHERE Status = 1 ");
            return dados.Any();
        }

        public async Task<bool> TemPendencias()
        {
            using var cn = CnRead;
            var pendencias = await cn.QueryAsync<Guid>(@" SELECT TOP 1 Id FROM Planilhas 
                                                          WHERE
                                                          AnoBaseId = @AnoBaseId
                                                          AND EhValido = 0 ", new { AnoBaseId = await _userResolver.GetYearIdAsync() }); ;

            return pendencias.Any();


        }

        public async Task<bool> TemSugestaoEnviadas(Guid usuarioId)
        {
            using var cn = CnRead;
            var sugestoes = await cn.QueryAsync<Guid>(@"SELECT Id FROM UsuarioAvaliador WHERE UsuarioId = @UsuarioId ", new { UsuarioId = usuarioId });
            return sugestoes.Any();
        }
    }
}
