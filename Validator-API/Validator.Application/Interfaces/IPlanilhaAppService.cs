using Validator.Domain.Commands.Planilhas;
using Validator.Domain.Core;
using Validator.Domain.Dtos;

namespace Validator.Application.Interfaces
{
    public interface IPlanilhaAppService
    {
        Task<ValidationResult> Updload(Stream excelStream);
        Task<ValidationResult> Remover(Guid id);
        Task<ValidationResult> Resolver(PlanilhaResolverPendenciaCommand command);
        Task<PendenciaDto> ObterPorId(Guid id);
    }
}
