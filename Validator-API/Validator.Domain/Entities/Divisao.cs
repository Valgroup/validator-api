using Validator.Domain.Core;
using Validator.Domain.Core.Interfaces;

namespace Validator.Domain.Entities
{
    public class Divisao : EntityWithAnoBase, IAnoBase
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }

        
    }
}
