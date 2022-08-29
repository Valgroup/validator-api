using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Domain.Commands;
using Validator.Domain.Commands.Usuarios;
using Validator.Domain.Core.Pagination;
using Validator.Domain.Dtos.Usuarios;

namespace Validator.Domain.Interfaces.Repositories
{
    public interface IUsuarioReadOnlyRepository
    {
        Task<UsuarioAuthDto> ObterPorId(Guid id);
        Task<IPagedResult<UsuarioDto>> Todos(UsuarioAdmConsultaCommand command);
        Task<IPagedResult<UsuarioDto>> ObterAvaliadores(UsuarioAdmConsultaCommand command);
        Task<IPagedResult<UsuarioSubordinadoDto>> ObterSubordinados(PaginationBaseCommand command);
    }
}
