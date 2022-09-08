namespace Validator.Domain.Commands.Usuarios
{
    public class AvaliadoresConsultaCommand
    {
        public int Page { get; set; }
        public int Take { get; set; } = 10;
        public int Skip { get { return Take * Page; } }
    }
}
