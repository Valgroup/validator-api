using Validator.Domain.Commands;
using Validator.Domain.Commands.Usuarios;
using Validator.Domain.Core.Pagination;
using Validator.Domain.Dtos.Usuarios;

namespace Validator.Domain.Interfaces.Repositories
{
    public interface IUsuarioReadOnlyRepository
    {
        Task<UsuarioAuthDto> ObterPorId(Guid id);
        Task<IPagedResult<UsuarioDto>> Todos(UsuarioAdmConsultaCommand command, Guid? avaliadorAntigoId = null);
        Task<IPagedResult<AvaliadorDto>> ObterAvaliadores(AvaliadoresConsultaCommand command);
        Task<IPagedResult<AvaliadorDto>> ObterAvaliadoresDetalhes(Guid avaliadoId);
        Task<IPagedResult<AvaliadorDto>> EscolherAvaliadores(AvaliadoresConsultaCommand command);
        Task<IPagedResult<AvaliadorDto>> ObterSugestaoAvaliadores(SugestaoAvaliadoresConsultaCommand command, Guid? avaliadorAntigoId = null);
        Task<IPagedResult<UsuarioSubordinadoDto>> ObterSubordinados(PaginationBaseCommand command);
        Task<UsuarioDto> ObterDetalhes(Guid id);
        Task<IPagedResult<UsuarioAprovacaoSubordinadoDto>> ObterAprovacaoSubordinados(AprovacaoSubordinadosCommand command);
        Task<IEnumerable<UsuarioDto>> TodosPorAno();
              

    }
}
