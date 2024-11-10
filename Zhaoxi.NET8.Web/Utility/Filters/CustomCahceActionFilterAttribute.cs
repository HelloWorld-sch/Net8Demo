using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static System.Net.Mime.MediaTypeNames;

namespace Zhaoxi.NET8.Web.Utility.Filters
{
    public class CustomCahceActionFilterAttribute : Attribute, IActionFilter
    {
        private static Dictionary<string, object> CacheDictionary = new Dictionary<string, object>();


        /// <summary>
        /// 在XX方法前执行
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string key = context.HttpContext.Request.Path;
            if (CacheDictionary.ContainsKey(key)) //说明没有缓存
            { 
                //断路器-只要是Result赋值，就不会继续往后执行
                context.Result = CacheDictionary[key] as IActionResult;
            }  
        }

        /// <summary>
        /// 在XX方法后执行
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //执行到这里，必然已经执行了Action了
            //
            string key = context.HttpContext.Request.Path;
            CacheDictionary[key] = context.Result;
        } 
    } 
}
