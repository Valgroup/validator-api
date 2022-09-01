using Microsoft.AspNetCore.Cors;
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

        //[EnableCors("CorsValidador")]
        //[RequestSizeLimit(525336576)]
        //[RequestFormLimits(MultipartBodyLengthLimit = 524288000)]
        [HttpPost, DisableRequestSizeLimit, Route("UploadXLS")]
        public async Task<IActionResult> UploadXLS()
        {
            try
            {
                var formFile = HttpContext.Request.Form.Files.FirstOrDefault();
                using (Stream stream = formFile.OpenReadStream())
                using (MemoryStream memory = new MemoryStream())
                {
                    await stream.CopyToAsync(memory);

                    var result = await _planilhaAppService.Updload(memory);
                    if (result.IsValid)
                        return await StatusCodeOK(result);

                    return await EntityValidation(result);
                }
            }
            catch(Exception e)
            {
                return Ok(e);
            }
            
        }
    }
}
