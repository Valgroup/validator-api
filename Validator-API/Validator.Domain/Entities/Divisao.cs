using Validator.Domain.Core;
using Validator.Domain.Core.Interfaces;

namespace Validator.Domain.Entities
{
    public class Divisao : EntityWithAnoBase, IAnoBase
    {
        protected Divisao() { }

        public Divisao(string nome)
        {
            Id = NewId;
            Nome = nome;
        }
        public Guid Id { get; private set; }
        public string Nome { get; private set; }


    }
}
