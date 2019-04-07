using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StoplightController.API.Controllers
{
    [Route("/manual")]
    public class ManualController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}