using Microsoft.AspNetCore.Mvc;
using Validator.API.Controllers.Common;
using Validator.Application.Interfaces;
using Validator.Domain.Core;

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

        [HttpPost, DisableRequestSizeLimit, Route("UploadXLS")]
        [ProducesResponseType(typeof(UploadResult), 200)]
        [ProducesResponseType(typeof(ValidationResult), 422)]
        public async Task<IActionResult> UploadXLS()
        {
            try
            {
                if (await _planilhaAppService.ProcessoInicializado())
                {
                    var res = new Domain.Core.ValidationResult();
                    res.Add("Não é possivel enviar mais planilha pois o Processo de Avaliadores foi inicializado!");
                    return await EntityValidation(res);
                }

                if (await _planilhaAppService.PossuiPendencias())
                {
                    var res = new Domain.Core.ValidationResult();
                    res.Add("A Planilha importada anteriormente ainda possui Pendências");
                    return await EntityValidation(res);
                }

                var formFile = HttpContext.Request.Form.Files.FirstOrDefault();
                using (Stream stream = formFile.OpenReadStream())
                using (MemoryStream memory = new MemoryStream())
                {
                    await stream.CopyToAsync(memory);

                    var result = await _planilhaAppService.Updload(memory);
                    if (result.IsValid)
                        return Ok(result);

                    return await EntityValidation(result);
                }
            }
            catch(Exception e)
            {
                var res = new ValidationResult();
                res.Add($"Ocorreu um erro ao processar a planilha, verifique se existe todas as colunas necessárias");
                return await EntityValidation(res);
            }
            
        }
    }
}
