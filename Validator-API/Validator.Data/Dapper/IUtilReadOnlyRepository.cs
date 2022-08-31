using Validator.Domain.Dtos;

namespace Validator.Data.Dapper
{
    public interface IUtilReadOnlyRepository
    {
        Task<IEnumerable<SelectedItemDto>> ObterTodosSetores();
        Task<IEnumerable<SelectedItemDto>> ObterTodasDivisoes();
    }
}
