using Microsoft.AspNetCore.Http;
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
            
            return File(stream, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName: $"Avaliador-{DateTime.Now.ToString("dd-MM-yyyy")}");
        }
    }
}
