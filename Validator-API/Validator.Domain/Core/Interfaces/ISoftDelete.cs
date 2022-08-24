namespace Validator.Domain.Core.Interfaces
{
    internal interface ISoftDelete
    {
        bool Deleted { get; set; }
    }
}
