using Validator.Domain.Core.Interfaces;
using Validator.Domain.Entities;

namespace Validator.Domain.Interfaces
{
    public interface IProcessoService
    {
        Task Atualizar(bool? temPendencia = null);
    }
}
