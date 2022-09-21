namespace Validator.Domain.Dtos.Usuarios
{
    public class UsuarioSubordinadoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Setor { get; set; }
        public string Divisao { get; set; }
        public string Status { get; set; }
        public int Total { get; set; }
    }
}
