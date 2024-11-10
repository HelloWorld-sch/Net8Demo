using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Zhaoxi.NET8.Web.Utility;

namespace Zhaoxi.NET8.Web.Controllers
{
    public class FourthController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly ConnectionStringOptions _ConnectionStringOptions;
        private readonly ConnectionStringOptions _ConnectionStringOptionsNew;
        public FourthController(IConfiguration configuration,IOptionsMonitor<ConnectionStringOptions> optionsMonitor,IOptions<ConnectionStringOptions> options)
        {
            Configuration = configuration;
            _ConnectionStringOptions = optionsMonitor.CurrentValue;
            _ConnectionStringOptionsNew = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            Console.WriteLine($"Id:{Configuration["Id"]}");
            Console.WriteLine($"Name:{Configuration["Name"]}");
            Console.WriteLine($"TeachInfo.Id:{Configuration["TeachInfo:Id"]}");
            Console.WriteLine($"TeachInfo.Name:{Configuration["TeachInfo:Name"]}");
            Console.WriteLine($"ConnectionStrings.WriteConnection:{Configuration["ConnectionStrings:WriteConnection"]}");
            Console.WriteLine($"ConnectionStrings.ReadConnectionList1:{Configuration["ConnectionStrings:ReadConnectionList:0"]}");
            Console.WriteLine($"ConnectionStrings.ReadConnectionList2:{Configuration["ConnectionStrings:ReadConnectionList:1"]}");
            Console.WriteLine($"ConnectionStrings.ReadConnectionList3:{Configuration["ConnectionStrings:ReadConnectionList:2"]}");

            return View();
        }
    }
}
