using Microsoft.AspNetCore.Mvc;
using Validator.API.Controllers.Common;
using Validator.Application.Interfaces;
using Validator.Data.Dapper;
using Validator.Domain.Commands;
using Validator.Domain.Commands.Planilhas;

namespace Validator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PendenciaController : ValidatorBaseController
    {
        private readonly IPlanilhaReadOnlyRepository _planilhaReadOnlyRepository;
        private readonly IPlanilhaAppService _planilhaAppService;
        public PendenciaController(IPlanilhaReadOnlyRepository planilhaReadOnlyRepository, IPlanilhaAppService planilhaAppService)
        {
            _planilhaReadOnlyRepository = planilhaReadOnlyRepository;
            _planilhaAppService = planilhaAppService;
        }

        [HttpPost, Route("Listar")]
        public async Task<IActionResult> Listar(PaginationBaseCommand command)
        {
            return Ok(await _planilhaReadOnlyRepository.ListarPendencias(command));
        }

        [HttpPut, Route("Resolver")]
        public async Task<IActionResult> Resolver([FromBody] PlanilhaResolverPendenciaCommand command)
        {
            var result = await _planilhaAppService.Resolver(command);
            if (result.IsValid)
                return await StatusCodeOK(result);

            return await EntityValidation(result);
        }

        [HttpDelete, Route("Remover/{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            var result = await _planilhaAppService.Remover(id);
            if (result.IsValid)
                return await StatusCodeOK(result);

            return await EntityValidation(result);
        }


    }
}
