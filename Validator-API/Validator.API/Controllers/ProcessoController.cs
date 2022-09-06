using Microsoft.AspNetCore.Mvc;
using Validator.API.Controllers.Common;
using Validator.Application.Interfaces;

namespace Validator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessoController : ValidatorBaseController
    {
        private readonly IDashAppService _dashAppService;

        public ProcessoController(IDashAppService dashAppService)
        {
            _dashAppService = dashAppService;
        }

        [HttpGet, Route("Inicializar")]
        public async Task<IActionResult> Inicializar()
        {
            var result = await _dashAppService.IniciarProcesso();
            if (result.IsValid)
                return await StatusCodeOK(result);

            return await EntityValidation(result);
        }
    }
}
