namespace Validator.Domain.Commands.Usuarios
{
    public class SubstituirAvaliadorCommand
    {
        public Guid AvaliadoId { get; set; }
        public Guid AvaliadorAntigoId { get; set; }
        public Guid AvaliadorNovoId { get; set; }
    }
}
