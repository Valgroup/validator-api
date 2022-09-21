namespace Validator.Domain.Commands.Usuarios
{
    public class SugestaoAvaliadoresConsultaCommand : AvaliadoresConsultaCommand
    {
        public Guid? DivisaoId { get; set; }
        public Guid? UsuarioId { get; set; }

    }
}
