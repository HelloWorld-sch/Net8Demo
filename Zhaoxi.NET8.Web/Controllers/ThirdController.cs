using Microsoft.AspNetCore.Mvc;
using Zhaoxi.NET8.Business.Interfaces;

namespace Zhaoxi.NET8.Web.Controllers
{
    /// <summary>
    ///  
    /// </summary>
    public class ThirdController : Controller
    {
        private readonly ILogger<ThirdController> _ILogger;
        private readonly ILoggerFactory _ILoggerFactory;
        private readonly IPhone _ApplePhone;

        public ThirdController(ILogger<ThirdController> iLogger, ILoggerFactory iLoggerFactory, IPhone applePhone)
        {
            _ILogger=iLogger;
            _ILoggerFactory=iLoggerFactory;
            _ApplePhone=applePhone;
        }
         
        public IActionResult Index()
        {
            ILogger<ThirdController> _Logger3 = _ILoggerFactory.CreateLogger<ThirdController>();
            _Logger3.LogInformation($"Index 被执行了。。。。。_Logger3");
            _Logger3.LogInformation($"Index 被执行了。。。");
            return new JsonResult(new { Success = true });
        }

        /// <summary>
        /// 明确参数是需要通过IOC容器来创建的----通过FromServices 来标记，完成方法的注入
        /// </summary>
        /// <param name="_applePhone"></param>
        /// <returns></returns>
        public IActionResult ReturnJson([FromServices]IPhone _applePhone)
        { 
            return new JsonResult(new { Success = true });
        }
    }
}
