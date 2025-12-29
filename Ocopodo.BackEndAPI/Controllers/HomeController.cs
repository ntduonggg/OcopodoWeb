using Microsoft.AspNetCore.Mvc;
using Ocopodo.BackEndAPI.Models;
using System.Diagnostics;

namespace Ocopodo.BackEndAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Ok();
        }

    }
}
