namespace Validator.Domain.Commands.Usuarios
{
    public class UsuarioAdmConsultaCommand
    {
        public Guid? DivisaoId { get; set; }
        public Guid? SetorId { get; set; }
        public string? QueryNome { get; set; }
       
        public int Page { get; set; }
        public int Take { get; set; } = 10;
        public int Skip { get { return Take * Page; } }
    }
}
