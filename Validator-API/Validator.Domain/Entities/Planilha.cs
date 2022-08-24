using Validator.Domain.Core;
using Validator.Domain.Core.Interfaces;

namespace Validator.Domain.Entities
{
    public class Planilha : EntityWithAnoBase, IAnoBase, ISoftDelete
    {
        public Planilha(string unidade, string nome, string email,
            string cargo, string nivel, DateTime dataAdmissao,
            string centroCusto, string numeroCentroCusto,
            string superiorImediato, string emailSuperior, string divisoes)
        {
            Id = NewId;
            Unidade = unidade;
            Nome = nome;
            Email = email;
            Cargo = cargo;
            Nivel = nivel;
            DataAdmissao = dataAdmissao;
            CentroCusto = centroCusto;
            NumeroCentroCusto = numeroCentroCusto;
            SuperiorImediato = superiorImediato;
            EmailSuperior = emailSuperior;
            Divisoes = divisoes;
        }

        public Guid Id { get; private set; }
        public string Unidade { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cargo { get; private set; }
        public string Nivel { get; private set; }
        public DateTime DataAdmissao { get; private set; }
        public string CentroCusto { get; private set; }
        public string NumeroCentroCusto { get; private set; }
        public string SuperiorImediato { get; private set; }
        public string EmailSuperior { get; private set; }
        public string Divisoes { get; private set; }
        public bool Deleted { get; set; }
    }
}
