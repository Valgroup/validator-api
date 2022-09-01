using System.Collections.Generic;
using Validator.Domain.Commands;
using Validator.Domain.Core.Pagination;
using Validator.Domain.Dtos.Dashes;
using Validator.Domain.Entities;

namespace Validator.Data.Dapper
{
    public interface IPlanilhaReadOnlyRepository
    {
        Task<IPagedResult<PlanilhaDto>> ListarPendencias(PaginationBaseCommand command);
        Task<IEnumerable<Planilha>> ObterTodas();
    }
}
