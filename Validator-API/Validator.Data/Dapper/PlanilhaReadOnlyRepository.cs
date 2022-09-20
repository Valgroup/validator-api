using Dapper;
using System.Text;
using Validator.Data.Repositories;
using Validator.Domain.Commands;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Pagination;
using Validator.Domain.Dtos;
using Validator.Domain.Dtos.Dashes;
using Validator.Domain.Entities;

namespace Validator.Data.Dapper
{
    public class PlanilhaReadOnlyRepository : BaseConnection, IPlanilhaReadOnlyRepository
    {
        private readonly IUserResolver _userResolver;

        public PlanilhaReadOnlyRepository(IUserResolver userResolver)
        {
            _userResolver = userResolver;
        }

        public async Task ExcluirProcessoAnoAtual(Guid anoBaseId)
        {
            using var cn = CnRead;

            await cn.ExecuteAsync(@" DELETE FROM UsuarioAvaliador WHERE UsuarioAvaliador.UsuarioId IN (SELECT U.Id FROM Usuarios U WHERE U.AnoBaseId = @AnoBaseId )
                                     DELETE FROM Usuarios WHERE Usuarios.AnoBaseId = @AnoBaseId AND Usuarios.Perfil != 1
                                     DELETE FROM Setor WHERE Setor.Id NOT IN (SELECT U.SetorId FROM Usuarios U WHERE U.AnoBaseId = @AnoBaseId )
                                     DELETE FROM Divisao WHERE Divisao.Id NOT IN (SELECT U.DivisaoId FROM Usuarios U WHERE U.AnoBaseId = @AnoBaseId )
                                     DELETE FROM Parametro WHERE Parametro.AnoBaseId = @AnoBaseId
                                     DELETE FROM Planilhas WHERE Planilhas.AnoBaseId = @AnoBaseId
                                     DELETE FROM Processos WHERE Processos.AnoBaseId = @AnoBaseId ", new { AnoBaseId = anoBaseId });


        }

        public async Task<IPagedResult<PlanilhaDto>> ListarDadosCarregados(PaginationBaseCommand command)
        {
            using var cn = CnRead;

            var sbQry = new StringBuilder(@"SELECT
	                                            *,
	                                        COUNT(1) OVER() AS Total
                                            FROM Planilhas
                                            WHERE
                                            EhValido = 1 ");

            if (!string.IsNullOrEmpty(command.QueryNome))
            {
                sbQry.Append(@"AND (
	                                Nome LIKE @WhereLike OR
	                                Email LIKE @WhereLike OR
	                                Unidade LIKE @WhereLike OR
	                                Cargo LIKE @WhereLike OR
	                                Nivel LIKE @WhereLike OR
	                                CentroCusto LIKE @WhereLike OR
	                                NumeroCentroCusto LIKE @WhereLike OR
	                                SuperiorImediato LIKE @WhereLike OR
	                                EmailSuperior LIKE @WhereLike OR
	                                CPF LIKE @WhereLike ) ");
            }

            sbQry.Append(" ORDER BY Nome OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY ");

            var planilhas = await cn.QueryAsync<PlanilhaDto>(sbQry.ToString(), new { command.Skip, command.Take, WhereLike = $"%{command.QueryNome}%" });

            return new PagedResult<PlanilhaDto>
            {
                Records = planilhas,
                RecordsFiltered = planilhas.Count(),
                RecordsTotal = planilhas.Any() ? planilhas.FirstOrDefault().Total : 0
            };
        }

        public async Task<IPagedResult<PlanilhaDto>> ListarPendencias(PaginationBaseCommand command)
        {
            using var cn = CnRead;

            var sbQry = new StringBuilder(@"SELECT
	                                            *,
	                                        COUNT(1) OVER() AS Total
                                            FROM Planilhas
                                            WHERE
                                            EhValido = 0 ");

            if (!string.IsNullOrEmpty(command.QueryNome))
            {
                sbQry.Append(@"AND (
	                                Nome LIKE @WhereLike OR
	                                Email LIKE @WhereLike OR
	                                Unidade LIKE @WhereLike OR
	                                Cargo LIKE @WhereLike OR
	                                Nivel LIKE @WhereLike OR
	                                CentroCusto LIKE @WhereLike OR
	                                NumeroCentroCusto LIKE @WhereLike OR
	                                SuperiorImediato LIKE @WhereLike OR
	                                EmailSuperior LIKE @WhereLike OR
	                                CPF LIKE @WhereLike ) ");
            }

            sbQry.Append(" ORDER BY Nome OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY ");

            var planilhas = await cn.QueryAsync<PlanilhaDto>(sbQry.ToString(), new { command.Skip, command.Take, WhereLike = $"%{command.QueryNome}%" });

            return new PagedResult<PlanilhaDto>
            {
                Records = planilhas,
                RecordsFiltered = planilhas.Count(),
                RecordsTotal = planilhas.Any() ? planilhas.FirstOrDefault().Total : 0
            };
        }

        public async Task<IEnumerable<PlanilhaExtraiDto>> ObterDadosExtracao()
        {
            using var cn = CnRead;

            return await cn.QueryAsync<PlanilhaExtraiDto>(@"SELECT 
                                                                U.Documento AS CPFAvaliado,
	                                                            CASE WHEN UAV.Deleted = 0 THEN UAV.Documento
		                                                             WHEN UAV.Deleted = 1 THEN 'Inativo' END AS CPFAvaliador,
	                                                            CASE WHEN U.SuperiorId != UA.AvaliadorId THEN 'PAR' END AS Tipo,
	                                                            CASE WHEN U.Deleted = 0 THEN 'Ativo'
		                                                             WHEN U.Deleted = 1 THEN 'Inativo' END AS Status

                                                            FROM UsuarioAvaliador UA
                                                            INNER JOIN Usuarios U ON U.Id = UA.UsuarioId
                                                            INNER JOIN Usuarios UAV ON UAV.Id = UA.AvaliadorId
                                                            WHERE
                                                            U.AnoBaseId = @AnoBaseId ", new { AnoBaseId = await _userResolver.GetYearIdAsync() }); ;
        }

        public async Task<IEnumerable<Planilha>> ObterTodas()
        {
            using var cn = CnRead;

            return await cn.QueryAsync<Planilha>(@"SELECT * FROM Planilhas WHERE AnoBaseId = @AnoBaseId ", new { AnoBaseId = await _userResolver.GetYearIdAsync() }); ;
        }


    }
}
