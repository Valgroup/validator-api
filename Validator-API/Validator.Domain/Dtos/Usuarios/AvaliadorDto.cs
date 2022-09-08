namespace Validator.Domain.Dtos.Usuarios
{
    public class AvaliadorDto
    {
        public Guid AvaliadorId { get; set; }
        public string Nome { get; set; }
        public string Setor { get; set; }
        public string Divisao { get; set; }
        public string Cargo { get; set; }
        public int Total { get; set; }
    }
}
