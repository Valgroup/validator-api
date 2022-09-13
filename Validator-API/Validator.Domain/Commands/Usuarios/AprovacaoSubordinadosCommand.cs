namespace Validator.Domain.Commands.Usuarios
{
    public class AprovacaoSubordinadosCommand : PaginationBaseCommand
    {
        public Guid SubordinadoId { get; set; }
    }
}
