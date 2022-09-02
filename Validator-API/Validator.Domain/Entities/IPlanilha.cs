namespace Validator.Domain.Entities
{
    public partial interface IPlanilha
    {
        string Cargo { get; }
        string CentroCusto { get; }
        DateTime DataAdmissao { get; }
        bool Deleted { get; set; }
        string Divisoes { get; }
        string Email { get; }
        string EmailSuperior { get; }
        Guid Id { get; }
        string Nivel { get; }
        string Nome { get; }
        string NumeroCentroCusto { get; }
        string SuperiorImediato { get; }
        string Unidade { get; }
    }

    public partial interface IPlanilha
    {

    }
}