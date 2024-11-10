using Microsoft.AspNetCore.Mvc.Filters;

namespace Zhaoxi.NET8.Web.Utility.Filters
{
    public class CustomResultFilterAttribute : Attribute, IResultFilter
    { 
        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("CustomResultFilterAttribute.OnResultExecuting");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("CustomResultFilterAttribute.OnResultExecuted");
        } 
    }


    /// <summary>
    /// 异步版本的实现
    /// </summary>
    public class CustomAsyncResultFilterAttribute : Attribute, IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            await next();
        }
    }
}
