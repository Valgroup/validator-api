using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Validator.Application.Interfaces;
using Validator.Domain.Commands.Logins;
using Validator.Domain.Core;
using Validator.Domain.Core.Models;

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
        [ProducesResponseType(typeof(LoginResultCommand), 200)]
        [ProducesResponseType(typeof(LoginResultCommand), 422)]
        public async Task<IActionResult> Post([FromBody] LoginCommand command)
        {
            try
            {
                var result = await _authAppService.Autenticar(command);
                if (result.IsValid)
                    return Ok(result);

                return StatusCode(422, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet, Route("Permissoes")]
        [ProducesResponseType(typeof(PermissaoJwt), 200)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _authAppService.Permissao());
        }

        [HttpGet, Route("RecuperarSenha")]
        [ProducesResponseType(typeof(ValidationResult), 200)]
        [ProducesResponseType(typeof(ValidationResult), 422)]
        [AllowAnonymous]
        public async Task<IActionResult> RecuperarSenha(string email)
        {
            var result = await _authAppService.RecuperarSenha(email);
            if (result.IsValid)
                return Ok(result);

            return StatusCode(422, result);
        }


    }
}
