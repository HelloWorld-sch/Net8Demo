using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Zhaoxi.NET8.Web.Models;

namespace Zhaoxi.NET8.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _IConfiguration;

        public HomeController(ILogger<HomeController> logger, IConfiguration iConfiguration)
        {
            _logger = logger;
            _IConfiguration = iConfiguration;
        }

        public IActionResult Index()
        {
            ViewBag.Prot = _IConfiguration["port"];
            return View();
        }

        public IActionResult DataJson()
        {
            return new JsonResult(new
            {
                Id = 123,
                UserName = "Richard",
                UserAge = 41
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
