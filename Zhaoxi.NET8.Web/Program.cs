using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.FileProviders;
using NLog.Extensions.Logging;
using System.Security.Claims;
using System.Text.Json;
using Zhaoxi.NET8.AutofacExtension.AOP;
using Zhaoxi.NET8.Business.Interfaces;
using Zhaoxi.NET8.Business.Services;
using Zhaoxi.NET8.Web.Utility;
using Zhaoxi.NET8.Web.Utility.AuthorizeExtension;
using Zhaoxi.NET8.Web.Utility.Filters;

namespace Zhaoxi.NET8.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //全局配置序列化器的规则
            builder.Services.ConfigureHttpJsonOptions(jsonoption =>
            {
                //jsonoption.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                //jsonoption.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                //jsonoption.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseUpper;
               jsonoption.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower;
                // jsonoption.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.KebabCaseUpper;  
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews(option =>
            {
                //option.Filters.Add<CustomActionFilterAttribute>(); //3. 全局注册---对当前项目下的所有的Action 都生效
            });

            builder.Services.AddSession();

            //log4net
            {
                ////nuget：log4net
                ////       Microsoft.Extensions.Logging.Log4Net.AspNetCore

                //builder.Logging.AddLog4Net("CfgFile/log4net.Config");
            }

            //Nlog
            {
                //Nuget引入：NLog.Web.AspNetCore
                builder.Logging.AddNLog("CfgFile/NLog.Config");
            }

            #region IOC
            {
                builder.Services.RegisterExt();
            }
            #endregion

            #region 整合Autofac
            //nuget：  autofac   Autofac.Extensions.DependencyInjection
            {
                builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
                builder.Host.ConfigureContainer<ContainerBuilder>(ContainerBuilder =>
                {
                    //在这个区域里面就是用来做服务注册的
                    ContainerBuilder.RegisterType<Microphone>().As<IMicrophone>();
                    ContainerBuilder.RegisterType<Power>().As<IPower>();
                    ContainerBuilder.RegisterType<Headphone>().As<IHeadphone>();
                    ContainerBuilder.RegisterType<ApplePhone>().As<IPhone>()
                        .EnableClassInterceptors();
                    ContainerBuilder.RegisterType<CustomInterceptor>();
                });
            }
            #endregion

            #region 读取配置文件
            {
                ////1.通过Configuration
                ReadConfiguration.Show(builder.Configuration);
                ////2.通过Configuration.Bind把配置文件信息绑定到实体对象中去
                ReadConfiguration.ShowBind(builder.Configuration);

                ////3.通过IServiceCollection配置，自动完成配置配置信息映射到和配置文件相同格式的实体中去
                ///Option模式
                builder.Services.Configure<ConnectionStringOptions>(builder.Configuration.GetSection("ConnectionStrings"));
            }
            #endregion


            builder.Services.AddTransient<CustomlogAsyncActionFilterAttribute>();
            builder.Services.AddTransient<CustomFilterFactoryAttribute>();


            #region 鉴权授权
            {
                //配置鉴权
                builder.Services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    option.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    option.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
                {
                    option.LoginPath = "/sixth/Login";//如果没有找到用户信息---鉴权失败--授权也失败了---就跳转到指定的Action 
                    option.AccessDeniedPath = "/sixth/Login";
                });

                builder.Services.AddAuthorization(option =>
                    {
                        option.AddPolicy("rolePolicy", policyBuilder =>
                        {
                            policyBuilder.RequireAssertion(context =>
                            {
                                //可以在这里做业务逻辑计算
                                bool bResult = context.User.HasClaim(c => c.Type == ClaimTypes.Role)
                                     && context.User.Claims.First(c => c.Type.Equals(ClaimTypes.Role)).Value == "Admin"
                                     && context.User.Claims.Any(c => c.Type == ClaimTypes.Name);

                                return bResult;
                            });
                            //获取到用户的信息之后，希望调用一些第三方的类来判定下呢？？  
                            policyBuilder.Requirements.Add(new CustomAuthorizationRequirement());

                            //实现一个接口 IAuthorizationHandler

                        });
                    });  //支持授权

                builder.Services.AddTransient<IMicrophone, Microphone>();
                builder.Services.AddTransient<IAuthorizationHandler, CustomAuthorizationHandler>();
            }
            #endregion


            var app = builder.Build();

            app.UseSession();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection(); //默认会转到https里面去
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
            }); //中间件，这里就是配置读取静态文件的地方

            app.UseRouting();

            app.UseAuthentication();  //鉴权中间件
            app.UseAuthorization();   // 授权的中间件

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
