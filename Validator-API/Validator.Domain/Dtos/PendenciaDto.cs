namespace Validator.Domain.Dtos
{
    public class PendenciaDto
    {
        public Guid Id { get; set; }
        public string? Unidade { get; set; }
        public string? CPF { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Cargo { get; set; }
        public string? Nivel { get; set; }
        public DateTime? DataAdmissao { get; set; }
        public string? CentroCusto { get; set; }
        public string? NumeroCentroCusto { get; set; }
        public string? SuperiorImediato { get; set; }
        public string? EmailSuperior { get; set; }
        public string? Validacoes { get; set; }
    }
}
