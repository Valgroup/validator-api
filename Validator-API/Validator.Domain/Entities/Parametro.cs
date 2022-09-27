using Validator.Domain.Core;
using Validator.Domain.Core.Interfaces;

namespace Validator.Domain.Entities
{
    public class Parametro : EntityWithAnoBase, IAnoBase
    {
        protected Parametro() { }

        public Parametro(int qtdeSugestaoMin, int qtdeSugestaoMax, int qtdeAvaliador, DateTime? dhFinalizacao)
        {
            Id = NewId;
            QtdeSugestaoMin = qtdeSugestaoMin;
            QtdeSugestaoMax = qtdeSugestaoMax;
            QtdeAvaliador = qtdeAvaliador;
            GerarDataFinalizacao(dhFinalizacao);
        }

        public Guid Id { get; private set; }
        public int QtdeSugestaoMin { get; private set; }
        public int QtdeSugestaoMax { get; private set; }
        public int QtdeAvaliador { get; private set; }
        public int QtdDiaFinaliza { get; private set; }
        public DateTime DhFinalizacao { get; private set; }

        public void Editar(int qtdeAvaliador, int qtdeSugestaoMin, int qtdeSugestaoMax, DateTime? dhFinalizacao)
        {
            QtdeAvaliador = qtdeAvaliador;
            QtdeSugestaoMin = qtdeSugestaoMin;
            QtdeSugestaoMax = qtdeSugestaoMax;
            GerarDataFinalizacao(dhFinalizacao);
            
        }

        private void GerarDataFinalizacao(DateTime? dhFinalizacao)
        {
            if (dhFinalizacao.HasValue)
            {
                DhFinalizacao = new DateTime(dhFinalizacao.Value.Year, dhFinalizacao.Value.Month, dhFinalizacao.Value.Day, 23, 59, 59);
            }
            else
            {
                var dh = DateTime.Now;
                DhFinalizacao = new DateTime(dh.Year, dh.Month, dh.Day, 23, 59, 59);
            }
        }
    }
}
