using Validator.Domain.Core.Enums;

namespace Validator.Domain.Dtos.Dashes
{
    public class DashResultadoQueryDto
    {
        public int QtdStatus { get; set; }
        public string StatusNome { get; set; }
        public int Total { get; set; }
        public int QtdUsuarios { get; set; }
    }
}
