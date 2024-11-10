using Microsoft.AspNetCore.Authorization;
using Zhaoxi.NET8.Business.Interfaces;

namespace Zhaoxi.NET8.Web.Utility.AuthorizeExtension
{
    public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement> // IAuthorizationHandler
    {

        private IMicrophone _IMicrophone;

        public CustomAuthorizationHandler(IMicrophone microphone)
        {
            this._IMicrophone = microphone;
        }

        /// <summary>
        /// 这个方法，用来校验用户信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
        {
            if (context.User != null && context.User.Claims.Count() > 0)
            {
                //有用户信息
                //在这里就可以进一步的判断 
                context.Succeed(requirement); //验证通过了
            }
            else
            {
                context.Fail();
            } 
            await Task.CompletedTask;
        }
    }
}
