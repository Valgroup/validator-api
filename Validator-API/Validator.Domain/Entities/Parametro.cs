using Validator.Domain.Core;
using Validator.Domain.Core.Interfaces;

namespace Validator.Domain.Entities
{
    public class Parametro : EntityWithAnoBase, IAnoBase
    {
        protected Parametro() { }

        public Parametro(int qtdeSugestaoMin, int qtdeSugestaoMax, int qtdeAvaliador, int qtdDiaFinaliza)
        {
            Id = NewId;
            QtdeSugestaoMin = qtdeSugestaoMin;
            QtdeSugestaoMax = qtdeSugestaoMax;
            QtdeAvaliador = qtdeAvaliador;
            QtdDiaFinaliza = qtdDiaFinaliza;

            CalcularDataFinalizacao();
        }

        public Guid Id { get; private set; }
        public int QtdeSugestaoMin { get; private set; }
        public int QtdeSugestaoMax { get; private set; }
        public int QtdeAvaliador { get; private set; }
        public int QtdDiaFinaliza { get; private set; }
        public DateTime DhFinalizacao { get; private set; }

        public void Editar(int qtdeAvaliador, int qtdeSugestaoMin, int qtdeSugestaoMax, int qtdDiaFinaliza)
        {
            QtdeAvaliador = qtdeAvaliador;
            QtdeSugestaoMin = qtdeSugestaoMin;
            QtdeSugestaoMax = qtdeSugestaoMax;
            QtdDiaFinaliza = qtdDiaFinaliza;

            CalcularDataFinalizacao();
        }

        private void CalcularDataFinalizacao()
        {
            var dh = DateTime.Now;
            if (QtdDiaFinaliza > 0)
                dh = dh.AddDays(QtdDiaFinaliza);

            DhFinalizacao = new DateTime(dh.Year, dh.Month, dh.Day, 23, 59, 59);
        }
    }
}
