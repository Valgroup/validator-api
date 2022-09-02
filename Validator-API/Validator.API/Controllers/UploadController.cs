using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
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


            var request = HttpContext.Request;

            if (!request.HasFormContentType ||
                !MediaTypeHeaderValue.TryParse(request.ContentType, out var mediaTypeHeader) ||
                string.IsNullOrEmpty(mediaTypeHeader.Boundary.Value))
            {
                return new UnsupportedMediaTypeResult();
            }

            var reader = new MultipartReader(mediaTypeHeader.Boundary.Value, request.Body);
            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition,
                    out var contentDisposition);

                if (hasContentDispositionHeader && contentDisposition.DispositionType.Equals("form-data") &&
                    !string.IsNullOrEmpty(contentDisposition.FileName.Value))
                {
                    var fileName = Path.GetRandomFileName();
                    var saveToPath = Path.Combine(Path.GetTempPath(), fileName);
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    using (var targetStream = System.IO.File.Create(saveToPath))
                    {
                        await section.Body.CopyToAsync(targetStream);
                        var result = await _planilhaAppService.Upload(targetStream);
                        //if (result.IsValid)
                        return Ok(result);

                        //return await EntityValidation(result);
                    }


                }

                section = await reader.ReadNextSectionAsync();
            }

            return BadRequest("No files data in the request.");

        }
    }
}
