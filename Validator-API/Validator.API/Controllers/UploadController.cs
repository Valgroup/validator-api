using Microsoft.AspNetCore.Mvc;
using Validator.API.Controllers.Common;
using Validator.Application.Interfaces;

namespace Validator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ValidatorBaseController
    {
        private readonly IPlanilhaAppService _planilhaAppService;

        public UploadController(IPlanilhaAppService planilhaAppService)
        {
            _planilhaAppService = planilhaAppService;
        }

        [HttpPost, Route("UploadXLS")]
        public async Task<IActionResult> UploadXLS()
        {
            var result = await _planilhaAppService.Updload(null);
            if (result.IsValid)
                return await StatusCodeOK(result);

            return await EntityValidation(result);
        }
    }
}
