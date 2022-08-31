using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Domain.Commands;
using Validator.Domain.Core.Pagination;
using Validator.Domain.Dtos.Dashes;

namespace Validator.Data.Dapper
{
    public interface IPlanilhaReadOnlyRepository
    {
        Task<IPagedResult<PlanilhaDto>> ListarPendencias(PaginationBaseCommand command);
    }
}
