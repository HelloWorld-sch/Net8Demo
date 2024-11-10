using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Zhaoxi.NET8.Web.Utility.Filters
{
    public class CustomCacheAsyncResourceFilterAttribute : Attribute, IAsyncResourceFilter
    {
        private static Dictionary<string, object> CacheDictionary = new Dictionary<string, object>();


        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            string key = context.HttpContext.Request.Path;
            if (!CacheDictionary.ContainsKey(key)) //说明没有缓存
            {
                ResourceExecutedContext resourceExecutedContext = await next.Invoke();  //
                CacheDictionary[key] = resourceExecutedContext.Result;
                context.Result= resourceExecutedContext.Result;
            }
            else
            {
                context.Result = CacheDictionary[key] as IActionResult;
            }
            await Task.CompletedTask;
        }
    }
}
