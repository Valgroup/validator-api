namespace Validator.Domain.Commands.Usuarios
{
    public class AvaliadoresConsultaCommand
    {
        public string? QueryNome { get; set; }
        public int Page { get; set; }
        public int Take { get; set; } = 10;
        public int Skip { get { return Take * Page; } }
    }
}
