namespace Validator.Domain.Core
{
    public class UploadResult : ValidationResult
    {
        public int QtdTotal { get; set; }
        public int QtdPendentes { get; set; }
    }
}
