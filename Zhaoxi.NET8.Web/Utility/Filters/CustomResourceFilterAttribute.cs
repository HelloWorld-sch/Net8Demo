using Microsoft.AspNetCore.Mvc.Filters;

namespace Zhaoxi.NET8.Web.Utility.Filters
{
    /// <summary>
    /// 是一个特性
    /// 实现IResourceFilter接口
    /// </summary>
    public class CustomResourceFilterAttribute : Attribute, IResourceFilter
    {
        /// <summary>
        /// 在XX资源之前发生
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("CustomResourceFilterAttribute.OnResourceExecuting");
        }

        /// <summary>
        /// 在XX资源之后发生
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("CustomResourceFilterAttribute.OnResourceExecuted");
        } 
    }


    //public class CustomAsyncResourceFilterAttribute : Attribute, IAsyncResourceFilter
    //{
    //    public Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
