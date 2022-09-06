using Validator.Domain.Core;
using Validator.Domain.Core.Enums;
using Validator.Domain.Core.Interfaces;

namespace Validator.Domain.Entities
{
    public class Processo : EntityWithAnoBase, IAnoBase
    {
        protected Processo() { }

        public Processo(ESituacaoProcesso situacao)
        {
            Id = NewId;
            Situacao = situacao;

        }

        public Guid Id { get; private set; }
        public ESituacaoProcesso Situacao { get; private set; }
        public DateTime? DhInicio { get; private set; }
        public DateTime? DhFim { get; private set; }


        public void InformarSituacao(bool temPendencia)
        {
            if (temPendencia)
                Situacao = ESituacaoProcesso.ComPendencia;
            else
                Situacao = ESituacaoProcesso.SemPendencia;
        }

        public void Inicializar()
        {
            Situacao = ESituacaoProcesso.Inicializada;
            DhInicio = DateTime.Now;
        }
    }
}
