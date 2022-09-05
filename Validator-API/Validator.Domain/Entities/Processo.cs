using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            InformarSituacao();
        }

        public Guid Id { get; private set; }
        public ESituacaoProcesso Situacao { get; private set; }
        public DateTime? DhInicio { get; private set; }
        public DateTime? DhFim { get; private set; }


        public void InformarSituacao()
        {
            switch (Situacao)
            {
                case ESituacaoProcesso.ComPendecia:
                    break;
                case ESituacaoProcesso.Inicializada:
                    DhInicio = DateTime.Now;
                    break;
                case ESituacaoProcesso.Finalizado:
                    DhFim = DateTime.Now;
                    break;
                default:
                    break;
            }
        }
    }
}
