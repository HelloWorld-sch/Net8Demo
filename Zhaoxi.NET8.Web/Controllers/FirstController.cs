using Microsoft.AspNetCore.Mvc;

namespace Zhaoxi.NET8.Web.Controllers
{
    public class FirstController : Controller
    { 
        //C： 业务逻辑处理
        //M： 模型视图
        //V： 视图  表现层
        public IActionResult Index()
        {
            //要做计算之后，传递一些数据给视图 
            {
                //有一对业务逻辑计算完了后 
            }
            ViewBag.User1 = "张三";
            ViewData["User2"] = "李四";
            TempData["User3"] = "王五";
             HttpContext.Session.SetString("User4", "赵六");
            object User5 = "田七"; 
            return View(User5);
        }
    }
}
