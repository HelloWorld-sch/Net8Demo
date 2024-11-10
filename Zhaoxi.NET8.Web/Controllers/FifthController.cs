using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Zhaoxi.NET8.Web.Models;
using Zhaoxi.NET8.Web.Utility;
using Zhaoxi.NET8.Web.Utility.Filters;

namespace Zhaoxi.NET8.Web.Controllers
{
     
    //[CustomFilterFactory(typeof(CustomlogAsyncActionFilterAttribute))]  //.2.控制器注册--对于当前的类下的所有方法都生效
    public class FifthController : Controller
    { 
        //一、ResourceFilter---天生就是为了缓存而存在的
        //如果遇到了框架给我们预留了接口或者是抽象类，如果我们要扩展，其实就是去实现接口，或者去继承抽象类

        //二、ActionFilter--提供了 //IActionFilter
        //IAsyncActionFilter
        //ActionFilterAttribute   都是抽象，不能直接使用；
        //ActionFilter适合做什么呢？  缓存？？？---可以做，但是不推荐
        // 适合记录日志~


        //三、ResultFilter

        //四、如果全局注册---所有的Action都会生效的；
        // 如果希望其中有一个不要生效呢？  希望能够避开几个？---Filter的匿名

        public FifthController()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[CustomCacheAsyncResourceFilter]
        //[CustomlogAsyncActionFilterAttribute] 
        //[TypeFilter(typeof(CustomlogAsyncActionFilterAttribute))]
        //[ServiceFilter(typeof(CustomlogAsyncActionFilterAttribute))]  //这种也可以，但是需要把Filter注册到IOC容器
        //[CustomFilterFactory(typeof(CustomlogAsyncActionFilterAttribute))]   //1. 方法标记--仅对当前方法生效

        //[CustomResultFilter]
        //[CustomAllowAnonymous]
        [CustomCacheAsyncResourceFilterAttribute]
        [CustomAlwaysRunResultFilterAttribute]  //计算是在前面的Filter 有终止操作，但是这个Filter还是会执行；----对于结果要有一个完善；要有一个统一的处理的时候，就会用到这个Filter
        public IActionResult Index()
        {
            //IResultFilter
            //IAsyncResourceFilter
            //ActionFilterAttribute

            //IActionFilter
            //IAsyncActionFilter
            //ActionFilterAttribute

            //IAlwaysRunResultFilter
            //IAsyncAlwaysRunResultFilter


             //IExceptionFilter


            //增加一个缓存？？
            //增加一个日志？？ 
            ViewData["user"] = new User
            {
                Id = new Random().Next(0, 100),
                Name = $"User-{new Random().Next(0, 100)}",
                Date = DateTime.Now
            }; 
            return View();
        }

        [TypeFilter(typeof(CustomExceptionFilterAttribute))]
        public IActionResult Index1()
        {
            int i = 0;
            int j = 1;
            int k = j / i;

            return View();
        }

        public IActionResult Index2()
        {
            return View();
        }
    }
}
