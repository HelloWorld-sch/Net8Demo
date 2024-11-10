﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace Zhaoxi.NET8.Web.Utility.Filters
{
    public class CustomlogAsyncActionFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly ILogger<CustomlogAsyncActionFilterAttribute> _ILogger;

        public CustomlogAsyncActionFilterAttribute(ILogger<CustomlogAsyncActionFilterAttribute> logger)
        {
            _ILogger = logger;
        }
         
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //在这里判断一下，如果标记的有匿名---可以让这里的逻辑都不执行，直接过去
            if (context.ActionDescriptor.EndpointMetadata.Any(c => c.GetType().Equals(typeof(CustomAllowAnonymousAttribute))))
            {
                //支持匿名
                await next();
            }
            else
            {
                var controllerName = context.RouteData.Values["controller"];
                var actionName = context.RouteData.Values["action"];
                {
                    _ILogger.LogInformation($"在{controllerName} {actionName}--方法执行前执行");
                }
                await next();
                {
                    _ILogger.LogInformation($"在{controllerName} {actionName}--方法执行后执行");
                }
            } 
        }
    }
}
