using Validator.Domain.Core;
using Validator.Domain.Core.Interfaces;

namespace Validator.Domain.Entities
{
    public class AnoBase : GuidId, ISoftDelete
    {
        protected AnoBase() { }

        public AnoBase(DateTime? dateTime)
        {
            AnoBaseId = NewId;
            Ano = dateTime.HasValue ? dateTime.Value.Year : DateTime.Now.Year;
        }

        public Guid AnoBaseId { get; private set; }
        public int Ano { get; private set; }
        public bool Deleted { get; set; }
    }
}
