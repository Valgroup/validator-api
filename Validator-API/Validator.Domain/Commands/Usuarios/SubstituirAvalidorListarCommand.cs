namespace Validator.Domain.Commands.Usuarios
{
    public class SubstituirAvalidorListarCommand : AvaliadoresConsultaCommand
    {
        public Guid AvaliadorId { get; set; }
        public Guid? UsuarioId { get; set; }
        public Guid? DivisaoId { get; set; }
       
    }
}
