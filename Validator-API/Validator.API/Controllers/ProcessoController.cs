using Microsoft.AspNetCore.Mvc;
using Validator.API.Controllers.Common;
using Validator.Application.Interfaces;
using Validator.Data.Dapper;
using Validator.Domain.Commands;
using Validator.Domain.Core.Helpers;
using Validator.Domain.Core.Pagination;
using Validator.Domain.Dtos.Dashes;

namespace Validator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessoController : ValidatorBaseController
    {
        private readonly IDashAppService _dashAppService;
        private readonly IPlanilhaReadOnlyRepository _planilhaReadOnlyRepository;

        public ProcessoController(IDashAppService dashAppService, IPlanilhaReadOnlyRepository planilhaReadOnlyRepository)
        {
            _dashAppService = dashAppService;
            _planilhaReadOnlyRepository = planilhaReadOnlyRepository;
        }

        [HttpGet, Route("Inicializar")]
        public async Task<IActionResult> Inicializar()
        {
            var url = $"http://matera/Avaliador/{RuntimeConfigurationHelper.Ambiente}/login";

            var result = await _dashAppService.IniciarProcesso(url);
            if (result.IsValid)
                return await StatusCodeOK(result);

            return await EntityValidation(result);
        }

        [HttpPost, Route("DadosCarregados")]
        [ProducesResponseType(typeof(PagedResult<PlanilhaDto>), 200)]
        public async Task<IActionResult> DadosCarregados(PaginationBaseCommand command)
        {
            return Ok(await _planilhaReadOnlyRepository.ListarDadosCarregados(command));
        }

        [HttpDelete, Route("Excluir")]
        public async Task<IActionResult> Excluir()
        {
            var result = await _dashAppService.ExcluirProcessoAnoAtual();
            if (result.IsValid)
                return await StatusCodeOK(result);

            return await EntityValidation(result);
        }
    }
}
