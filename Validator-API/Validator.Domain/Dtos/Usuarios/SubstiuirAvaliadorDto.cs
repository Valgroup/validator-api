namespace Validator.Domain.Dtos.Usuarios
{
    public class SubstiuirAvaliadorDto
    {
        public Guid AvaliadorAntigoId { get; set; }
        public string AvaliadorAntigoNome { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public IEnumerable<AvaliadorDto>? Records { get; set; }
    }
}
