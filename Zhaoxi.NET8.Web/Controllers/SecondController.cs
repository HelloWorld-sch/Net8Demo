using Microsoft.AspNetCore.Mvc;

namespace Zhaoxi.NET8.Web.Controllers
{
    /// <summary>
    /// 如果要做日志记录
    /// </summary>
    public class SecondController : Controller
    {
        private readonly ILogger<SecondController> _ILogger;
        private readonly ILoggerFactory _ILoggerFactory;

        /// <summary>
        ///执行构造函数---自动创建出构造函数的参数---传递过来
        ///依赖注入---构造函数注入
        ///
        ///要创建很多实例---对象  new 
        /// </summary>
        /// <param name="iLogger"></param>
        /// <param name="iLoggerFactory"></param>
        public SecondController(ILogger<SecondController> iLogger, ILoggerFactory iLoggerFactory)
        {
            _ILogger=iLogger;
            _ILoggerFactory=iLoggerFactory;
            _ILogger.LogInformation(" LogInformation; this is Second 构造函数~");
            _ILogger.LogError("LogError: this is Second 构造函数~");
            _ILogger.LogWarning("LogWarning: this is Second 构造函数~");
            _ILogger.LogDebug("LogDebug:this is Second 构造函数~");

            _ILogger.LogInformation("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&~");
            ILogger<SecondController> _loger= _ILoggerFactory.CreateLogger<SecondController>();
            _loger.LogInformation(" LogInformation; this is Second 构造函数~");
            _loger.LogError("LogError: this is Second 构造函数~");
            _loger.LogWarning("LogWarning: this is Second 构造函数~");
            _loger.LogDebug("LogDebug:this is Second 构造函数~");


        }
         
        public IActionResult Index()
        {
            ILogger<SecondController> _Logger3 = _ILoggerFactory.CreateLogger<SecondController>();
            _Logger3.LogInformation($"Index 被执行了。。。。。_Logger3");
            _Logger3.LogInformation($"Index 被执行了。。。");
            return new JsonResult(new { Success = true });
        }
    }
}
