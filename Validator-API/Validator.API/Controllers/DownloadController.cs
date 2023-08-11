using Microsoft.AspNetCore.Mvc;
using Validator.Application.Interfaces;

namespace Validator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly IPlanilhaAppService _planilhaAppService;

        public DownloadController(IPlanilhaAppService planilhaAppService)
        {
            _planilhaAppService = planilhaAppService;
        }

        [HttpGet]
        public async Task<IActionResult> Download()
        {
            var stream = await _planilhaAppService.GerarAvaliacao();
            //application/vnd.ms-excel
            //application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
            return File(stream, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName: $"Avaliador-{DateTime.Now.ToString("dd-MM-yyyy-HH-mm")}");
        }
    }
}
