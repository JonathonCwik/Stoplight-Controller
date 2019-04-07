using Microsoft.AspNetCore.Mvc;

namespace StoplightController.API.Controllers
{
    public class HomeController : Controller
    {
        // GET
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}