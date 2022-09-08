using Microsoft.AspNetCore.Mvc;
using Validator.API.Controllers.Common;
using Validator.Application.Interfaces;
using Validator.Domain.Commands;
using Validator.Domain.Commands.Usuarios;
using Validator.Domain.Core.Pagination;
using Validator.Domain.Dtos.Usuarios;
using Validator.Domain.Interfaces.Repositories;

namespace Validator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ValidatorBaseController
    {
        private readonly IUsuarioReadOnlyRepository _usuarioReadOnlyRepository;
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuarioController(IUsuarioReadOnlyRepository usuarioReadOnlyRepository, IUsuarioAppService usuarioAppService)
        {
            _usuarioReadOnlyRepository = usuarioReadOnlyRepository;
            _usuarioAppService = usuarioAppService;
        }

        [HttpPost, Route("Todos")]
        [ProducesResponseType(typeof(PagedResult<UsuarioDto>), 200)]
        public async Task<IActionResult> Todos(UsuarioAdmConsultaCommand command)
        {
            return Ok(await _usuarioReadOnlyRepository.Todos(command));
        }

        [HttpPost, Route("Avaliadores")]
        public async Task<IActionResult> Avaliadores(AvaliadoresConsultaCommand command)
        {
            return Ok(await _usuarioReadOnlyRepository.ObterAvaliadores(command));
        }

        [HttpPost, Route("SugestaoAvaliadores")]
        public async Task<IActionResult> SugestaoAvaliadores(SugestaoAvaliadoresConsultaCommand command)
        {
            return Ok(await _usuarioReadOnlyRepository.ObterSugestaoAvaliadores(command));
        }

        [HttpPost, Route("EscolherAvaliadores")]
        public async Task<IActionResult> EscolherAvaliadores(List<Guid> avaliadoresId)
        {
            var result = await _usuarioAppService.EscolherAvaliadores(avaliadoresId);
            if (result.IsValid)
                return await StatusCodeOK(result);

            return await EntityValidation(result);
        }

        [HttpPost, Route("Subordinados")]
        public async Task<IActionResult> Subordinados(PaginationBaseCommand command)
        {
            return Ok(await _usuarioReadOnlyRepository.ObterSubordinados(command));
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _usuarioAppService.DeleteAsync(id);
            if (result.IsValid)
                return await StatusCodeOK(result);

            return await EntityValidation(result);
        }
    }
}
