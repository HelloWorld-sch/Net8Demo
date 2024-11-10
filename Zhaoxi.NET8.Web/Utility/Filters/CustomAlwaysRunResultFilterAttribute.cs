using Microsoft.AspNetCore.Mvc.Filters;

namespace Zhaoxi.NET8.Web.Utility.Filters
{
    public class CustomAlwaysRunResultFilterAttribute : Attribute, IAlwaysRunResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
             
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
             
        }
    }
}
