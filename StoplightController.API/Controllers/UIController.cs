using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace StoplightController.API.Controllers
{
    [Route("/")]
    public class UIController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("manual")]
        public IActionResult Manual()
        {
            return View();
        }

        [HttpGet("random")]
        public IActionResult Random()
        {
            return View();
        }
    }
}