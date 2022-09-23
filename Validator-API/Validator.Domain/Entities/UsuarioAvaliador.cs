using Validator.Domain.Core;
using Validator.Domain.Core.Enums;

namespace Validator.Domain.Entities
{
    public class UsuarioAvaliador : GuidId
    {
        protected UsuarioAvaliador() { }

        public UsuarioAvaliador(Guid usuarioId, Guid avaliadorId)
        {
            UsuarioId = usuarioId;
            AvaliadorId = avaliadorId;
            Status = EStatuAvaliador.Pendente;
            Id = NewId;
            DataHora = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Guid AvaliadorId { get; private set; }
        public DateTime DataHora { get; private set; }
        public EStatuAvaliador Status { get; private set; }
        public virtual Usuario Usuario { get; private set; }
        public virtual Usuario Avaliador { get; private set; }

        public void AlterarAvaliador(Guid avaliadorNovoId)
        {
            AvaliadorId = avaliadorNovoId;
        }

        public void Aprovar()
        {
            Status = EStatuAvaliador.Confirmada;
        }
    }
}
