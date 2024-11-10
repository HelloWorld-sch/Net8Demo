using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Zhaoxi.NET8.Web.Utility.Filters
{
    public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
    {

        private readonly IModelMetadataProvider _IModelMetadataProvider;

        public CustomExceptionFilterAttribute(IModelMetadataProvider iModelMetadataProvider)
        {
            _IModelMetadataProvider=iModelMetadataProvider;
        }
        /// <summary>
        /// 当有异常发生的时候，就来执行这个方法
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnException(ExceptionContext context)
        {
            //在这里处理异常
            if (context.ExceptionHandled == false)
            {
                //1. ajax请求    json
                //2. 请求页面    异常页面提示
                if (IsAjaxRequest(context.HttpContext.Request))
                {
                    context.Result = new JsonResult(new
                    {
                        Succeess = false,
                        Message = context.Exception.Message
                    });
                }
                else
                {
                    //返回页面
                    ViewResult result = new ViewResult { ViewName = "~/Views/Shared/Error.cshtml" };
                    result.ViewData = new ViewDataDictionary(_IModelMetadataProvider, context.ModelState);
                    result.ViewData.Add("Exception", context.Exception);
                    context.Result = result; //断路器---只要对Result赋值--就不继续往后了； 
                } 
                context.ExceptionHandled = true;// 表示当前异常被处理掉了
            }
        }


        private bool IsAjaxRequest(HttpRequest request)
        {
            //HttpWebRequest httpWebRequest = null;
            //httpWebRequest.Headers.Add("X-Requested-With", "XMLHttpRequest"); 
            string header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }
    }
}
