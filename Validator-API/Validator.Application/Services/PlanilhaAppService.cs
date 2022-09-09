using ExcelDataReader;
using System.Text;
using Validator.Application.Interfaces;
using Validator.Data.Dapper;
using Validator.Domain.Commands.Planilhas;
using Validator.Domain.Core;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Dtos;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;

namespace Validator.Application.Services
{
    public class PlanilhaAppService : AppBaseService, IPlanilhaAppService
    {
        private readonly IPlanilhaService _planilhaService;
        private readonly IPlanilhaReadOnlyRepository _planilhaReadOnlyRepository;
        private readonly IProcessoService _processoService;
        public PlanilhaAppService(IUnitOfWork unitOfWork, IPlanilhaService planilhaService, IPlanilhaReadOnlyRepository planilhaReadOnlyRepository, IProcessoService processoService) : base(unitOfWork)
        {
            _planilhaService = planilhaService;
            _planilhaReadOnlyRepository = planilhaReadOnlyRepository;
            _processoService = processoService;
        }

        public async Task<PendenciaDto> ObterPorId(Guid id)
        {
            var planilha = await _planilhaService.GetByIdAsync(id);
            if (planilha != null)
            {
                return new PendenciaDto
                {
                    Id = planilha.Id,
                    Cargo = planilha.Cargo,
                    CentroCusto = planilha.CentroCusto,
                    CPF = planilha.CPF,
                    DataAdmissao = planilha.DataAdmissao,
                    Email = planilha.Email,
                    EmailSuperior = planilha.EmailSuperior,
                    Nivel = planilha.Nivel,
                    Nome = planilha.Nome,
                    NumeroCentroCusto = planilha.NumeroCentroCusto,
                    SuperiorImediato = planilha.SuperiorImediato,
                    Unidade = planilha.Unidade,
                    Validacoes = planilha.Validacoes
                };
            }

            return null;
        }

        public async Task<ValidationResult> Remover(Guid id)
        {
            var planilha = _planilhaService.GetById(id);
            if (planilha == null)
            {
                ValidationResult.Add("Registro não encontrado");
                return ValidationResult;
            }

            _planilhaService.Delete(planilha);

            await CommitAsync();

            return ValidationResult;

        }

        public async Task<ValidationResult> Resolver(PlanilhaResolverPendenciaCommand command)
        {
            var planilha = _planilhaService.GetById(command.Id);
            if (planilha == null)
            {
                ValidationResult.Add("Registro não encontrado");
                return ValidationResult;
            }

            planilha.Resolver(command);

            _planilhaService.Update(planilha);

            await CommitAsync();

            await _processoService.Atualizar();

            await CommitAsync();

            return ValidationResult;
        }

        public async Task<ValidationResult> Updload(Stream excelStream)
        {
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var reader = ExcelReaderFactory.CreateReader(excelStream, new ExcelReaderConfiguration { FallbackEncoding = Encoding.UTF8 }))
            {
                var dataSet = reader.AsDataSet();
                var table = dataSet.Tables[0];
                var planilhas = new List<Planilha>();
                var planilhasAtualizar = new List<Planilha>();
                var todas = await _planilhaReadOnlyRepository.ObterTodas();

                for (int i = 1; i < table.Rows.Count; i++)
                {
                    var nome = table.Rows[i][0]?.ToString();
                    var email = table.Rows[i][1]?.ToString();
                    var cpf = table.Rows[i][2]?.ToString();
                    var cargo = table.Rows[i][3]?.ToString();
                    var nivel = table.Rows[i][4]?.ToString();
                    var dataAdmissao = table.Rows[i][5]?.ToString();
                    var centroCusto = table.Rows[i][6]?.ToString();
                    var numeroCentro = table.Rows[i][7]?.ToString();
                    var unidade = table.Rows[i][8]?.ToString();
                    var superior = table.Rows[i][9]?.ToString();
                    var emailSuperior = table.Rows[i][10]?.ToString();
                    var direcao = table.Rows[i][11]?.ToString();

                    if (string.IsNullOrEmpty(email))
                        continue;

                    DateTime? dataAdm = null;
                    if (!dataAdmissao.Contains("-"))
                        dataAdm = Convert.ToDateTime(dataAdmissao);

                    var planilha = todas.FirstOrDefault(f => f.Email == email);
                    if (planilha != null)
                    {
                        planilha.Alterar(unidade, nome, cargo, nivel, dataAdm, centroCusto, numeroCentro, superior, emailSuperior, direcao, cpf);
                        planilhasAtualizar.Add(planilha);
                    }
                    else
                    {
                        planilhas.Add(new Planilha(unidade, nome, email, cargo, nivel, dataAdm, centroCusto, numeroCentro, superior, emailSuperior, direcao, cpf));
                    }
                }

                if (planilhas.Any())
                    await _planilhaService.CreateRangeAsync(planilhas);

                if (planilhasAtualizar.Any())
                    _planilhaService.UpdateRange(planilhasAtualizar);

                var temPendencias = (planilhas.Any(a => !a.EhValido) || planilhasAtualizar.Any(a => !a.EhValido));
                await _processoService.Atualizar(temPendencias);

                await CommitAsync();

            }

            return ValidationResult;
        }
    }
}
