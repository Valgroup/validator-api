namespace Validator.Domain.Commands.Usuarios
{
    public class ExcluirSugestaoAvaliadoCommand
    {
        public Guid AvaliadoId { get; set; }
        public Guid AvaliadorId { get; set; }
    }
}
