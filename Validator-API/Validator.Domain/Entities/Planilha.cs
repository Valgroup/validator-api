using Validator.Domain.Commands.Planilhas;
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
            Validar();
        }

        public Guid Id { get; private set; }
        public string? Unidade { get; private set; }
        public string? CPF { get; private set; }
        public string? Nome { get; private set; }
        public string? Email { get; private set; }
        public string? Cargo { get; private set; }
        public string? Nivel { get; private set; }
        public DateTime? DataAdmissao { get; private set; }
        public string? CentroCusto { get; private set; }
        public string? NumeroCentroCusto { get; private set; }
        public string? SuperiorImediato { get; private set; }
        public string? EmailSuperior { get; private set; }
        public string? Direcao { get; private set; }
        public bool EhValido { get; private set; }
        public string? Validacoes { get; private set; }
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

            if (string.IsNullOrEmpty(SuperiorImediato))
                validacoes.Add(MensagemResource.EhObrigatorio("Superior Imediato"));

            if (string.IsNullOrEmpty(EmailSuperior))
                validacoes.Add(MensagemResource.EhObrigatorio("E-mail Superior"));

            Validacoes = string.Join(" | ", validacoes);

            EhValido = !validacoes.Any();
        }

        public void Resolver(PlanilhaResolverPendenciaCommand command)
        {
            Unidade = command.Unidade;
            CPF = command.CPF;
            Nome = command.Nome;
            Email = command.Email;
            Cargo = command.Cargo;
            Nivel = command.Nivel;
            SuperiorImediato = command.SuperiorImediato;
            EmailSuperior = command.EmailSuperior;

            Validar();

        }

        public void Alterar(string? unidade, string? nome, string? email, string? cargo, string? nivel, DateTime? dataAdm, string? centroCusto, string? numeroCentro, string? superior, string? emailSuperior, string? direcao)
        {
            Unidade = unidade.TrimOrDefault();
            Nome = nome.TrimOrDefault();
            Email = email.TrimOrDefault();
            Cargo = cargo.TrimOrDefault();
            Nivel = nivel.TrimOrDefault();
            DataAdmissao = dataAdm;
            CentroCusto = centroCusto.TrimOrDefault();
            NumeroCentroCusto = numeroCentro.TrimOrDefault();
            SuperiorImediato = superior.TrimOrDefault();
            EmailSuperior = emailSuperior.TrimOrDefault();
            Direcao = direcao.TrimOrDefault();
            Validar();
        }
    }
}
