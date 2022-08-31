using Validator.Domain.Core;
using Validator.Domain.Core.Enums;
using Validator.Domain.Core.Interfaces;

namespace Validator.Domain.Entities
{
    public class UsuarioAvaliador
    {
        protected UsuarioAvaliador() { }

        public UsuarioAvaliador(Guid usuarioId, Guid avaliadorId)
        {
            UsuarioId = usuarioId;
            AvaliadorId = avaliadorId;
            Status = EStatuAvaliador.Enviada;
        }

        public Guid UsuarioId { get; private set; }
        public Guid AvaliadorId { get; private set; }
        public EStatuAvaliador Status { get; private set; }
        public virtual Usuario Usuario { get; private set; }
        public virtual Usuario Avaliador { get; private set; }
    }
}
