using Dapper;
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

        public async Task<IEnumerable<Planilha>> ObterTodas()
        {
            using var cn = CnRead;

            return await cn.QueryAsync<Planilha>(@"SELECT * FROM Planilhas WHERE AnoBaseId = @AnoBaseId ", new { AnoBaseId = await _userResolver.GetYearIdAsync() }); ;
        }

        
    }
}
