using Microsoft.AspNetCore.Mvc.Filters;

namespace Zhaoxi.NET8.Web.Utility.Filters
{
    public class CustomActionFilterAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// 在XX方法前执行
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("CustomActionFilterAttribute.OnActionExecuting");
        }

        /// <summary>
        /// 在XX方法后执行
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("CustomActionFilterAttribute.OnActionExecuted");
        } 
    }


    public class CustomAsyncActionFilterAttribute : Attribute, IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            throw new NotImplementedException();
        }
    }


    //public class CustomAsyncActionNewFilterAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuted(ActionExecutedContext context)
    //    {
    //        base.OnActionExecuted(context);
    //    }
    //}
}
