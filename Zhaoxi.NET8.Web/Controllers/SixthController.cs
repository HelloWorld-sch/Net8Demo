using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Zhaoxi.NET8.Web.Models;
using Zhaoxi.NET8.Web.Utility;
using Zhaoxi.NET8.Web.Utility.Filters;

namespace Zhaoxi.NET8.Web.Controllers
{
    public class SixthController : Controller
    {
        public SixthController()
        {

        }

        // [Authorize(Roles = "Admin")]  //如果在解析用户信息的时候，在Cookies中只要是解析到了用户信息，就直接允许了，就授权了；

        [Authorize(Policy = "rolePolicy")] //在授权的时候，要按照这个策略来进行判定
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User,Admin")]  //两者是或者的关系，满足一个即可

        //[Authorize(Roles = "User")]
        //[Authorize(Roles = "Admin")] //标记两个，两者必须同时生效才能授权通过
        public IActionResult Index1()
        {
            return View("~/Views/Sixth/index.cshtml");
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult Index2()
        {
            return View("~/Views/Sixth/index.cshtml");
        }


        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录提交
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(string name, string password)
        {
            if ("Richard".Equals(name) && "1".Equals(password))  //在正式环境是需要去链接数据库校验的
            {
                //查询到用户的信息

                List<Claim> claims = new List<Claim>()//鉴别你是谁，相关信息
                    {
                        new Claim("Userid","1"),
                        new Claim(ClaimTypes.Role,"Admin"),
                        new Claim(ClaimTypes.Role,"User"),
                        new Claim(ClaimTypes.Name,$"{name}--来自于Cookies"),
                        new Claim(ClaimTypes.Email,$"18672713698@163.com"),
                        new Claim("password",password),//可以写入任意数据
                        new Claim("Account","Administrator"),
                        new Claim("role","admin"),
                        new Claim("QQ","1030499676")
                    };

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Customer"));

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30),//过期时间：30分钟

                }).Wait();
                var user = HttpContext.User;
                return base.Redirect("/sixth/Index");
            }
            else
            {
                base.ViewBag.Msg = "用户或密码错误";
            }
            return await Task.FromResult<IActionResult>(View());
        }
    }
}
