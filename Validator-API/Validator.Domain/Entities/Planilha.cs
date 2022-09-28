using System.Text.RegularExpressions;
using Validator.Domain.Commands.Planilhas;
using Validator.Domain.Core;
using Validator.Domain.Core.Extensions;
using Validator.Domain.Core.Helpers;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Core.Resources;

namespace Validator.Domain.Entities
{
    public class Planilha : EntityWithAnoBase, IAnoBase
    {
        protected Planilha() { }

        public Planilha(string unidade, string nome, string email,
            string nivel, DateTime? dataAdmissao,
            string centroCusto, string numeroCentroCusto,
            string superiorImediato, string emailSuperior, string direcao, string cpf)
        {
            Id = NewId;
            Unidade = unidade.TrimOrDefault();
            Nome = nome.TrimOrDefault();
            Email = email.TrimOrDefault();
            Nivel = nivel.TrimOrDefault();
            Cargo = Nivel;
            CPF = string.IsNullOrEmpty(cpf) ? null : Regex.Replace(cpf, @"[^\d]", "");
            DataAdmissao = dataAdmissao;
            CentroCusto = centroCusto.ClearCaracters(new char[] { '-' }); ;
            NumeroCentroCusto = numeroCentroCusto.TrimOrDefault();
            SuperiorImediato = superiorImediato.ClearCaracters(new char[] { '-' });
            EmailSuperior = emailSuperior.ClearCaracters(new char[] { '-' });
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

            //if (!string.IsNullOrEmpty(CPF))
            //{
            //    if (!ValidadorHelper.CPFEhValido(CPF))
            //        validacoes.Add("CPF inválido");
            //}

            if (string.IsNullOrEmpty(Nome))
                validacoes.Add(MensagemResource.EhObrigatorio(nameof(Nome)));

            if (string.IsNullOrEmpty(Email))
                validacoes.Add(MensagemResource.EhObrigatorio(nameof(Email)));

            if (!string.IsNullOrEmpty(Email))
            {
                if (!ValidadorHelper.ValidarEmail(Email))
                    validacoes.Add("E-mail do Usuário inválido");
            }

            if (string.IsNullOrEmpty(Nivel))
                validacoes.Add(MensagemResource.EhObrigatorio(nameof(Nivel)));

            if (string.IsNullOrEmpty(CentroCusto))
                validacoes.Add(MensagemResource.EhObrigatorio("Centro de Custo"));

            if (string.IsNullOrEmpty(Direcao))
            {
                if (string.IsNullOrEmpty(SuperiorImediato))
                    validacoes.Add(MensagemResource.EhObrigatorio("Superior Imediato"));

                if (string.IsNullOrEmpty(EmailSuperior))
                    validacoes.Add(MensagemResource.EhObrigatorio("E-mail do Superior"));

                if (!string.IsNullOrEmpty(EmailSuperior))
                {
                    if (!ValidadorHelper.ValidarEmail(EmailSuperior))
                        validacoes.Add("E-mail do superior inválido");
                }
            }

            Validacoes = string.Join(" | ", validacoes);

            EhValido = !validacoes.Any();
        }

        public void Resolver(PlanilhaResolverPendenciaCommand command)
        {
            Unidade = command.Unidade;
            CPF = string.IsNullOrEmpty(command.CPF) ? null : Regex.Replace(command.CPF, @"[^\d]", "");
            Nome = command.Nome;
            Email = command.Email;
            Nivel = command.Nivel;
            Cargo = command.Nivel;
            SuperiorImediato = command.SuperiorImediato.ClearCaracters(new char[] { '-' });
            EmailSuperior = command.EmailSuperior.ClearCaracters(new char[] { '-' });

            Validar();

        }

        public void Alterar(string? unidade, string? nome, string? nivel,
            DateTime? dataAdm, string? centroCusto, string? numeroCentro, string? superior, string? emailSuperior, string? direcao, string? cpf, string? email)
        {
            if (!string.IsNullOrEmpty(cpf))
                cpf = cpf.Replace("Não tem Registro", "");

            Unidade = unidade.TrimOrDefault();
            Nome = nome.TrimOrDefault();
            Email = email.TrimOrDefault();
            Nivel = nivel.TrimOrDefault();
            Cargo = Nivel;
            CPF = string.IsNullOrEmpty(cpf) ? null : Regex.Replace(cpf, @"[^\d]", "");
            DataAdmissao = dataAdm;
            CentroCusto = centroCusto.ClearCaracters(new char[] { '-' }); ;
            NumeroCentroCusto = numeroCentro.TrimOrDefault();
            SuperiorImediato = superior.ClearCaracters(new char[] { '-' });
            EmailSuperior = emailSuperior.ClearCaracters(new char[] { '-' });
            Direcao = direcao.TrimOrDefault();
            Validar();
        }
    }
}
