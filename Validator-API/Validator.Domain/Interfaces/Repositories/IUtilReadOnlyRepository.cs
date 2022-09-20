using Validator.Domain.Dtos;

namespace Validator.Domain.Interfaces.Repositories
{
    public interface IUtilReadOnlyRepository
    {
        Task<IEnumerable<SelectedItemDto>> ObterTodosSetores();
        Task<IEnumerable<SelectedItemDto>> ObterTodasDivisoes();
        Task<bool> TemPendencias();
        Task<bool> TemDadosExportacao();
        
    }
}
