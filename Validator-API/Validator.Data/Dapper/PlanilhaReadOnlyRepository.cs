using Dapper;
using System.Text;
using Validator.Data.Repositories;
using Validator.Domain.Commands;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Pagination;
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

        public async Task<IPagedResult<PlanilhaDto>> ListarPendencias(PaginationBaseCommand command)
        {
            using var cn = CnRead;

            var sbQry = new StringBuilder(@"SELECT
	                                            *,
	                                        COUNT(1) OVER() AS Total
                                            FROM Planilhas
                                            WHERE
                                            EhValido = 0 AND Deleted = 0 ");

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

        public async Task<IEnumerable<Planilha>> ObterTodas()
        {
            using var cn = CnRead;

            return await cn.QueryAsync<Planilha>(@"SELECT * FROM Planilhas WHERE AnoBaseId = @AnoBaseId AND Deleted = 0 ", new { AnoBaseId = await _userResolver.GetYearIdAsync() }); ;
        }


    }
}
