
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Zhaoxi.NET8.WebCore;

namespace Zhaoxi.NET8.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddSwaggerExt();

            JWTTokenOptions tokenOptions = new JWTTokenOptions();
            builder.Configuration.Bind("JWTTokenOptions", tokenOptions);
            builder.Services
                .AddAuthorization() //启用授权
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>  //这里是配置的鉴权的逻辑
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         //JWT有一些默认的属性，就是给鉴权时就可以筛选了
                         ValidateIssuer = true,//是否验证Issuer
                         ValidateAudience = true,//是否验证Audience
                         ValidateLifetime = true,//是否验证失效时间
                         ValidateIssuerSigningKey = true,//是否验证SecurityKey
                         ValidAudience = tokenOptions.Audience,//
                         ValidIssuer = tokenOptions.Issuer,//Issuer，这两项和前面签发jwt的设置一致
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
                         AudienceValidator = (m, n, z) =>
                         {
                             //这里可以写自己定义的验证逻辑
                             //return m != null && m.FirstOrDefault().Equals(builder.Configuration["audience"]);  
                             return true;
                         },
                         LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                         {
                             //return notBefore <= DateTime.Now
                             //&& expires >= DateTime.Now;
                             ////&& validationParameters

                             return true;

                         }//自定义校验规则
                     };
                 });


            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerExt();
            }

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
