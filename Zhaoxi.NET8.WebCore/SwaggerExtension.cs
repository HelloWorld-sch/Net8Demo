using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Zhaoxi.NET8.WebCore
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerExt(this IServiceCollection Service)
        {
            Service.AddEndpointsApiExplorer();
            Service.AddSwaggerGen(c =>
           {
               #region 展示注释
               {
                   //获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                   string basePath = AppContext.BaseDirectory;
                   string xmlPath = Path.Combine(basePath, "Zhaoxi.NET8.WebApi.xml");
                   c.IncludeXmlComments(xmlPath);
               }
               #endregion

               #region 版本控制
               {
                   foreach (FieldInfo field in typeof(ApiVersionInfo).GetFields())
                   {
                       c.SwaggerDoc(field.Name, new OpenApiInfo()
                       {
                           Title = $"{field.Name}:这里是朝夕Core WebApi~",
                           Version = field.Name,
                           Description = $"coreWebApi {field.Name} 版本"
                       });
                   }
               }
               #endregion

               #region 支持Swagger传递Token
               {
                   c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                   {
                       Description = "请输入token,格式为 Bearer xxxxxxxx（注意中间必须有空格）",
                       Name = "Authorization",
                       In = ParameterLocation.Header,
                       Type = SecuritySchemeType.ApiKey,
                       BearerFormat = "JWT",
                       Scheme = "Bearer"
                   });

                   //添加安全要求
                   c.AddSecurityRequirement(new OpenApiSecurityRequirement
                   {
                      {
                            new OpenApiSecurityScheme
                            {
                                Reference =new OpenApiReference()
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id ="Bearer"
                                }
                            },
                            new string[]{ }
                        }
                   });
               }
               #endregion
           });

        }


        public static void UseSwaggerExt(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (FieldInfo field in typeof(ApiVersionInfo).GetFields())
                {
                    c.SwaggerEndpoint($"/swagger/{field.Name}/swagger.json", $"{field.Name}");
                }
            });
        }
    }
}
