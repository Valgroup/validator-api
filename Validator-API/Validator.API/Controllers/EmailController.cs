using Microsoft.AspNetCore.Mvc;

namespace Validator.API.Controllers
{
    public class EmailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
