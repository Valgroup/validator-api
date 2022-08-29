namespace Validator.Domain.Dtos.Usuarios
{
    public class UsuarioDto
    {
        public Guid Id  { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Setor { get; set; }
        public string Unidade { get; set; }
        public string Divisao { get; set; }
        public string Cargo { get; set; }
        public string Superior { get; set; }
        public int Total { get; set; }
    }
}
