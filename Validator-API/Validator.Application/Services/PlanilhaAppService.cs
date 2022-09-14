using ClosedXML.Excel;
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

        public async Task<bool> PossuiPendencias()
        {
            var processo = await _processoService.GetByCurrentYear();
            if (processo == null)
                return false;

            return processo.Situacao == Domain.Core.Enums.ESituacaoProcesso.ComPendencia;
        }

        public async Task<bool> ProcessoInicializado()
        {
            var processo = await _processoService.GetByCurrentYear();
            if (processo == null)
                return false;

            return processo.Situacao == Domain.Core.Enums.ESituacaoProcesso.Inicializada;
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

            await _processoService.Atualizar();

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

            if (!planilha.EhValido)
            {
                ValidationResult.Add(planilha.Validacoes);
                return ValidationResult;
            }

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
                    var nivel = table.Rows[i][3]?.ToString();
                    var dataAdmissao = table.Rows[i][4]?.ToString();
                    var centroCusto = table.Rows[i][5]?.ToString();
                    var unidade = table.Rows[i][6]?.ToString();
                    var superior = table.Rows[i][7]?.ToString();
                    var emailSuperior = table.Rows[i][8]?.ToString();
                    var direcao = table.Rows[i][9]?.ToString();

                    DateTime? dataAdm = null;
                    if (!dataAdmissao.Contains("-"))
                        dataAdm = Convert.ToDateTime(dataAdmissao);

                    var planilha = todas.FirstOrDefault(f => f.Email == email);
                    if (planilha != null)
                    {
                        planilha.Alterar(unidade, nome, nivel, dataAdm, centroCusto, null, superior, emailSuperior, direcao, cpf, email);
                        planilhasAtualizar.Add(planilha);
                    }
                    else
                    {
                        planilhas.Add(new Planilha(unidade, nome, email, nivel, dataAdm, centroCusto, null, superior, emailSuperior, direcao, cpf));
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

        public async Task<byte[]> GerarAvaliacao()
        {
            using (var workBook = new XLWorkbook())
            {
                var ws = workBook.Worksheets.Add(DateTime.Now.Year.ToString());
                ws.Cell("A1").Value = "";
                ws.Cell("B1").Value = "Avaliação 180";
                ws.Cell("C1").Value = "";
                ws.Cell("D1").Value = "";

                var cabecalhoGeral = ws.Range("A1:D1");
                cabecalhoGeral.Style.Fill.BackgroundColor = XLColor.FromHtml("#243193");
                cabecalhoGeral.Style.Font.FontColor = XLColor.White;
                cabecalhoGeral.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                cabecalhoGeral.Style.Border.TopBorderColor = XLColor.Black;
                cabecalhoGeral.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                cabecalhoGeral.Style.Border.BottomBorderColor = XLColor.Black;
                cabecalhoGeral.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                cabecalhoGeral.Style.Border.LeftBorderColor = XLColor.Black;
                cabecalhoGeral.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                cabecalhoGeral.Style.Border.RightBorderColor = XLColor.Black;
                cabecalhoGeral.Style.Border.InsideBorder = XLBorderStyleValues.None;
                cabecalhoGeral.Style.Border.InsideBorderColor = XLColor.Transparent;

                ws.Cell("A2").Value = "CPF Avaliado";
                ws.Cell("B2").Value = "CPF Avaliador";
                ws.Cell("C2").Value = "Tipo do Avaliador";
                ws.Cell("D2").Value = "Status";

                var cabecalho = ws.Range("A2:D2");
                cabecalho.Style.Fill.BackgroundColor = XLColor.Gray;
                cabecalho.Style.Font.FontColor = XLColor.Black;
                cabecalho.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                cabecalho.Style.Border.TopBorderColor = XLColor.Black;
                cabecalho.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                cabecalho.Style.Border.BottomBorderColor = XLColor.Black;
                cabecalho.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                cabecalho.Style.Border.LeftBorderColor = XLColor.Black;
                cabecalho.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                cabecalho.Style.Border.RightBorderColor = XLColor.Black;
                cabecalho.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                cabecalho.Style.Border.InsideBorderColor = XLColor.Black;

                var dados = await _planilhaReadOnlyRepository.ObterDadosExtracao();
                int linha = 3;

                foreach (var item in dados)
                {
                    ws.Cell($"A{linha}").Value = item.CPFAvaliado;
                    ws.Cell($"B{linha}").Value = item.CPFAvaliador;
                    ws.Cell($"C{linha}").Value = item.Tipo;
                    ws.Cell($"D{linha}").Value = item.Status;

                    var linhaEstilo = ws.Range($"A{linha}:D{linha}");
                    
                    linhaEstilo.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    linhaEstilo.Style.Border.TopBorderColor = XLColor.Black;
                    linhaEstilo.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    linhaEstilo.Style.Border.BottomBorderColor = XLColor.Black;
                    linhaEstilo.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    linhaEstilo.Style.Border.LeftBorderColor = XLColor.Black;
                    linhaEstilo.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    linhaEstilo.Style.Border.RightBorderColor = XLColor.Black;
                    linhaEstilo.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    linhaEstilo.Style.Border.InsideBorderColor = XLColor.Black;

                    linhaEstilo.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                    linha++;
                }

                ws.Columns().AdjustToContents();

                using var ms = new MemoryStream();
                workBook.SaveAs(ms);
                ms.Position = 0;
                return ms.ToArray();
            }

        }

    }
}
