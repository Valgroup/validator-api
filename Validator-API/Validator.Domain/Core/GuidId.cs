namespace Validator.Domain.Core
{
    public class GuidId
    {
        public GuidId()
        {
            NewId = Guid.NewGuid();
        }

        public Guid NewId { get; }
    }
}
