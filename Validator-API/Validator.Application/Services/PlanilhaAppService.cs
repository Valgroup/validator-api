using ExcelDataReader;
using System.Data;
using System.Text;
using Validator.Application.Interfaces;
using Validator.Domain.Commands.Planilhas;
using Validator.Domain.Core;
using Validator.Domain.Core.Interfaces;
using Validator.Domain.Entities;
using Validator.Domain.Interfaces;

namespace Validator.Application.Services
{
    public class PlanilhaAppService : AppBaseService, IPlanilhaAppService
    {
        private readonly IPlanilhaService _planilhaService;
        public PlanilhaAppService(IUnitOfWork unitOfWork, IPlanilhaService planilhaService) : base(unitOfWork)
        {
            _planilhaService = planilhaService;
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

            return ValidationResult;
        }

        public async Task<ValidationResult> Updload(Stream excelStream)
        {
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var reader = ExcelReaderFactory.CreateReader(excelStream, new ExcelReaderConfiguration { FallbackEncoding = Encoding.UTF8 }))
            {
                var dataSet = reader.AsDataSet();
                var table = dataSet.Tables[2];
                var planilhas = new List<Planilha>();

                for (int i = 1; i < table.Rows.Count; i++)
                {
                    var nome = table.Rows[i][0].ToString();
                    var email = table.Rows[i][1].ToString();
                    var cpf = table.Rows[i][2].ToString();
                    var cargo = table.Rows[i][3].ToString();
                    var nivel = table.Rows[i][4].ToString();
                    var dataAdmissao = table.Rows[i][5].ToString();
                    var centroCusto = table.Rows[i][6].ToString();
                    var numeroCentro = table.Rows[i][7].ToString();
                    var unidade = table.Rows[i][8].ToString();
                    var superior = table.Rows[i][9].ToString();
                    var emailSuperior = table.Rows[i][10].ToString();
                    var direcao = table.Rows[i][11].ToString();

                    DateTime? dataAdm = null;
                    if (!dataAdmissao.Contains("-"))
                        dataAdm = Convert.ToDateTime(dataAdmissao);

                    planilhas.Add(new Planilha(unidade, nome, email, cargo, nivel, dataAdm, centroCusto, numeroCentro, superior, emailSuperior, direcao));
                }

                await _planilhaService.CreateRangeAsync(planilhas);

                await CommitAsync();

            }


            return ValidationResult;
        }
    }
}
