using Validator.Domain.Core;
using Validator.Domain.Core.Enums;
using Validator.Domain.Core.Interfaces;

namespace Validator.Domain.Entities
{
    public class Progresso : EntityWithAnoBase, IAnoBase
    {
        protected Progresso() { }

        public Progresso(Guid usuarioId, EStatuAvaliador status)
        {
            Id = NewId;
            UsuarioId = usuarioId;
            Status = status;
        }

        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public EStatuAvaliador Status { get; private set; }
        public virtual Usuario Usuario { get; private set; }

    }
}
