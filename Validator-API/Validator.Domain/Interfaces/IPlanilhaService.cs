using Validator.Domain.Core.Interfaces;
using Validator.Domain.Entities;

namespace Validator.Domain.Interfaces
{
    public interface IPlanilhaService : IServiceDomain<Planilha>
    {
        Task CreateRangeAsync(List<Planilha> entities);
        void UpdateRange(List<Planilha> entities);

    }
}
