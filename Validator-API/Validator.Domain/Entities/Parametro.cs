using Validator.Domain.Core;
using Validator.Domain.Core.Interfaces;

namespace Validator.Domain.Entities
{
    public class Parametro : EntityWithAnoBase, IAnoBase
    {
        protected Parametro() { }

        public Parametro(int qtdeSugestaoMin, int qtdeSugestaoMax, int qtdeAvaliador)
        {
            Id = NewId;
            QtdeSugestaoMin = qtdeSugestaoMin;
            QtdeSugestaoMax = qtdeSugestaoMax;
            QtdeAvaliador = qtdeAvaliador;
        }

        public Guid Id { get; private set; }
        public int QtdeSugestaoMin { get; private set; }
        public int QtdeSugestaoMax { get; private set; }
        public int QtdeAvaliador { get; private set; }

        public void Editar(int qtdeAvaliador, int qtdeSugestaoMin, int qtdeSugestaoMax)
        {
            QtdeAvaliador = qtdeAvaliador;
            QtdeSugestaoMin = qtdeSugestaoMin;
            QtdeSugestaoMax = qtdeSugestaoMax;
        }
    }
}
