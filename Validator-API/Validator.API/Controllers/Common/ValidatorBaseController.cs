using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Validator.Domain.Core;

namespace Validator.API.Controllers.Common
{

    public class ValidatorBaseController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> EntityValidation(ValidationResult result)
        {
            return await Task.FromResult(StatusCode((int)HttpStatusCode.UnprocessableEntity, result));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> StatusCodeOK(ValidationResult result)
        {
            return await Task.FromResult(StatusCode((int)HttpStatusCode.OK, result));
        }
    }
}
