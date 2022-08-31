using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Validator.API.Controllers.Common;
using Validator.Data.Dapper;

namespace Validator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilController : ValidatorBaseController
    {
        private readonly IUtilReadOnlyRepository _utilReadOnlyRepository;

        public UtilController(IUtilReadOnlyRepository utilReadOnlyRepository)
        {
            _utilReadOnlyRepository = utilReadOnlyRepository;
        }

        [HttpGet, Route("Divisoes")]
        public async Task<IActionResult> TodasDivisoes()
        {
            return Ok(await _utilReadOnlyRepository.ObterTodasDivisoes());
        }

        [HttpGet, Route("Setores")]
        public async Task<IActionResult> Setores()
        {
            return Ok(await _utilReadOnlyRepository.ObterTodosSetores());
        }
    }
}
