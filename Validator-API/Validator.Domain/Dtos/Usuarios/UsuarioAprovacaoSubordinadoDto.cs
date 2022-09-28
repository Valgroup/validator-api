namespace Validator.Domain.Dtos.Usuarios
{
    public class UsuarioAprovacaoSubordinadoDto
    {
        public Guid Id { get; set; }
        public string Subordinado { get; set; }
        public string SugestaoAvaliador { get; set; }
        public string StatusAvaliador { get; set; }
        public string Setor { get; set; }
        public string Unidade { get; set; }
        public int QtdeSugestao { get; set; }
        public int QtdeAvaliacaoConfirmada { get; set; }
        public int Total { get; set; }
    }
}
