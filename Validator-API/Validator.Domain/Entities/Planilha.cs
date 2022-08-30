using Validator.Domain.Core;
using Validator.Domain.Core.Extensions;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Resources;

namespace Validator.Domain.Entities
{
    public class Planilha : EntityWithAnoBase, IAnoBase, ISoftDelete
    {
        protected Planilha() { }

        public Planilha(string unidade, string nome, string email,
            string cargo, string nivel, DateTime? dataAdmissao,
            string centroCusto, string numeroCentroCusto,
            string superiorImediato, string emailSuperior, string direcao)
        {
            Id = NewId;
            Unidade = unidade.TrimOrDefault();
            Nome = nome.TrimOrDefault();
            Email = email.TrimOrDefault();
            Cargo = cargo.TrimOrDefault();
            Nivel = nivel.TrimOrDefault();
            DataAdmissao = dataAdmissao;
            CentroCusto = centroCusto.TrimOrDefault();
            NumeroCentroCusto = numeroCentroCusto.TrimOrDefault();
            SuperiorImediato = superiorImediato.TrimOrDefault();
            EmailSuperior = emailSuperior.TrimOrDefault();
            Direcao = direcao.TrimOrDefault();
        }

        public Guid Id { get; private set; }
        public string Unidade { get; private set; }
        public string CPF { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cargo { get; private set; }
        public string Nivel { get; private set; }
        public DateTime? DataAdmissao { get; private set; }
        public string CentroCusto { get; private set; }
        public string NumeroCentroCusto { get; private set; }
        public string SuperiorImediato { get; private set; }
        public string EmailSuperior { get; private set; }
        public string Direcao { get; private set; }
        public bool EhValido { get; private set; }
        public string Validacoes { get; private set; }
        public bool Deleted { get; set; }
        public void Validar()
        {
            var validacoes = new List<string>();

            if (string.IsNullOrEmpty(Unidade))
                validacoes.Add(MensagemResource.EhObrigatorio(nameof(Unidade)));

            if (string.IsNullOrEmpty(CPF))
                validacoes.Add(MensagemResource.EhObrigatorio(nameof(CPF)));

            if (string.IsNullOrEmpty(Nome))
                validacoes.Add(MensagemResource.EhObrigatorio(nameof(Nome)));

            if (string.IsNullOrEmpty(Email))
                validacoes.Add(MensagemResource.EhObrigatorio(nameof(Email)));

            if (string.IsNullOrEmpty(Cargo))
                validacoes.Add(MensagemResource.EhObrigatorio(nameof(Cargo)));

            if (string.IsNullOrEmpty(Nivel))
                validacoes.Add(MensagemResource.EhObrigatorio(nameof(Nivel)));





            EhValido = !validacoes.Any();
        }
    }
}
