using Validator.Domain.Entities;

namespace Validator.Domain.Core
{
    public class EntityWithAnoBase : GuidId
    {
        public Guid AnoBaseId { get; set; }
        public virtual AnoBase AnoBase { get; private set; }

       
    }
}
