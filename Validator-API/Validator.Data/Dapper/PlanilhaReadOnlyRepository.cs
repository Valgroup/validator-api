using Dapper;
using Validator.Data.Repositories;
using Validator.Domain.Commands;
using Validator.Domain.Core.Pagination;
using Validator.Domain.Dtos.Dashes;

namespace Validator.Data.Dapper
{
    public class PlanilhaReadOnlyRepository : BaseConnection, IPlanilhaReadOnlyRepository
    {
        public async Task<IPagedResult<PlanilhaDto>> ListarPendencias(PaginationBaseCommand command)
        {
            using var cn = CnRead;

            var planilhas = await cn.QueryAsync<PlanilhaDto>(@"SELECT
	                                                             *,
	                                                             COUNT(1) OVER() AS Total
                                                               FROM Planilhas
                                                               WHERE
                                                                 EhValido = 0
                                                                 AND Deleted = 0
                                                                 ORDER BY Nome OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY ", new { command.Skip, command.Take });



            return new PagedResult<PlanilhaDto>
            {
                Records = planilhas,
                RecordsFiltered = planilhas.Count(),
                RecordsTotal = planilhas.Any() ? planilhas.FirstOrDefault().Total : 0
            };
        }
    }
}
