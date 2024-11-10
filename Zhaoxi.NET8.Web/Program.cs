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

            //ȫ���������л����Ĺ���
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
                //option.Filters.Add<CustomActionFilterAttribute>(); //3. ȫ��ע��---�Ե�ǰ��Ŀ�µ����е�Action ����Ч
            });

            builder.Services.AddSession();

            //log4net
            {
                ////nuget��log4net
                ////       Microsoft.Extensions.Logging.Log4Net.AspNetCore

                //builder.Logging.AddLog4Net("CfgFile/log4net.Config");
            }

            //Nlog
            {
                //Nuget���룺NLog.Web.AspNetCore
                builder.Logging.AddNLog("CfgFile/NLog.Config");
            }

            #region IOC
            {
                builder.Services.RegisterExt();
            }
            #endregion

            #region ����Autofac
            //nuget��  autofac   Autofac.Extensions.DependencyInjection
            {
                builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
                builder.Host.ConfigureContainer<ContainerBuilder>(ContainerBuilder =>
                {
                    //��������������������������ע���
                    ContainerBuilder.RegisterType<Microphone>().As<IMicrophone>();
                    ContainerBuilder.RegisterType<Power>().As<IPower>();
                    ContainerBuilder.RegisterType<Headphone>().As<IHeadphone>();
                    ContainerBuilder.RegisterType<ApplePhone>().As<IPhone>()
                        .EnableClassInterceptors();
                    ContainerBuilder.RegisterType<CustomInterceptor>();
                });
            }
            #endregion

            #region ��ȡ�����ļ�
            {
                ////1.ͨ��Configuration
                ReadConfiguration.Show(builder.Configuration);
                ////2.ͨ��Configuration.Bind�������ļ���Ϣ�󶨵�ʵ�������ȥ
                ReadConfiguration.ShowBind(builder.Configuration);

                ////3.ͨ��IServiceCollection���ã��Զ��������������Ϣӳ�䵽�������ļ���ͬ��ʽ��ʵ����ȥ
                ///Optionģʽ
                builder.Services.Configure<ConnectionStringOptions>(builder.Configuration.GetSection("ConnectionStrings"));
            }
            #endregion


            builder.Services.AddTransient<CustomlogAsyncActionFilterAttribute>();
            builder.Services.AddTransient<CustomFilterFactoryAttribute>();


            #region ��Ȩ��Ȩ
            {
                //���ü�Ȩ
                builder.Services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    option.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    option.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
                {
                    option.LoginPath = "/sixth/Login";//���û���ҵ��û���Ϣ---��Ȩʧ��--��ȨҲʧ����---����ת��ָ����Action 
                    option.AccessDeniedPath = "/sixth/Login";
                });

                builder.Services.AddAuthorization(option =>
                    {
                        option.AddPolicy("rolePolicy", policyBuilder =>
                        {
                            policyBuilder.RequireAssertion(context =>
                            {
                                //������������ҵ���߼�����
                                bool bResult = context.User.HasClaim(c => c.Type == ClaimTypes.Role)
                                     && context.User.Claims.First(c => c.Type.Equals(ClaimTypes.Role)).Value == "Admin"
                                     && context.User.Claims.Any(c => c.Type == ClaimTypes.Name);

                                return bResult;
                            });
                            //��ȡ���û�����Ϣ֮��ϣ������һЩ�������������ж����أ���  
                            policyBuilder.Requirements.Add(new CustomAuthorizationRequirement());

                            //ʵ��һ���ӿ� IAuthorizationHandler

                        });
                    });  //֧����Ȩ

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

            app.UseHttpsRedirection(); //Ĭ�ϻ�ת��https����ȥ
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
            }); //�м��������������ö�ȡ��̬�ļ��ĵط�

            app.UseRouting();

            app.UseAuthentication();  //��Ȩ�м��
            app.UseAuthorization();   // ��Ȩ���м��

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
