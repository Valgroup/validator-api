using Microsoft.AspNetCore.Mvc;
using Validator.API.Controllers.Common;
using Validator.Application.Interfaces;
using Validator.Domain.Commands.Dashes;

namespace Validator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashController : ValidatorBaseController
    {
        private readonly IDashAppService _paramentroAppService;

        public DashController(IDashAppService paramentroAppService)
        {
            _paramentroAppService = paramentroAppService;
        }

        [HttpPost, Route("SalvarParametros")]
        public async Task<IActionResult> Salvar([FromBody] ParametroSalvarCommand command)
        {
            var result = await _paramentroAppService.AdicionarOuAtualizar(command);
            if (result.IsValid)
                return await StatusCodeOK(result);

            return await EntityValidation(result);
        }

        [HttpPost, Route("ObterParametros")]
        public async Task<IActionResult> Obter()
        {
            return Ok(await _paramentroAppService.ObterParametros());
        }

        [HttpPost, Route("Resultados")]
        public async Task<IActionResult> Resultados(ConsultarResultadoCommand command)
        {
            return Ok(await _paramentroAppService.ObterResultados(command));

        }

        [HttpGet, Route("Permissoes")]
        public async Task<IActionResult> Permissoes()
        {
            return Ok(await _paramentroAppService.ObterPermissao());

        }


    }
}
