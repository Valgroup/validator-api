using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Validator.Application.Interfaces;
using Validator.Domain.Commands.Logins;

namespace Validator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthAppService _authAppService;

        public LoginController(IAuthAppService authAppService)
        {
            _authAppService = authAppService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] LoginCommand command)
        {
            var result = await _authAppService.Autenticar(command);
            if (result.IsValid)
                return Ok(result);

            return StatusCode(422, result);
        }
    }
}
