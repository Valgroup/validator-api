namespace Validator.Domain.Core.Interfaces
{
    public interface ISelfValidation
    {
        public bool IsEntityValid { get; set; }
        public ValidationResult ValidationResult { get; set; }
    }
}
