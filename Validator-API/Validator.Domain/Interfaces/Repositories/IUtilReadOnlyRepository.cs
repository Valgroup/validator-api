using Validator.Domain.Dtos;

namespace Validator.Domain.Interfaces.Repositories
{
    public interface IUtilReadOnlyRepository
    {
        Task<IEnumerable<SelectedItemDto>> ObterTodosSetores();
        Task<IEnumerable<SelectedItemDto>> ObterTodasDivisoes();
        Task<bool> TemPendencias();
        Task<bool> TemDadosExportacao();
        Task<bool> TemSugestaoEnviadas(Guid usuarioId);
        Task<int> ObterQtdPendetes();
        Task<int> ObterQtdTotal();

    }
}
