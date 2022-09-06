using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Validator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessoController : ControllerBase
    {
        [HttpGet, Route("Inicializar")]
        public async Task<IActionResult> Inicializar()
        {
            return Ok();
        }
    }
}
