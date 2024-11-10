
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
                .AddAuthorization() //������Ȩ
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>  //���������õļ�Ȩ���߼�
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         //JWT��һЩĬ�ϵ����ԣ����Ǹ���Ȩʱ�Ϳ���ɸѡ��
                         ValidateIssuer = true,//�Ƿ���֤Issuer
                         ValidateAudience = true,//�Ƿ���֤Audience
                         ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                         ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                         ValidAudience = tokenOptions.Audience,//
                         ValidIssuer = tokenOptions.Issuer,//Issuer���������ǰ��ǩ��jwt������һ��
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
                         AudienceValidator = (m, n, z) =>
                         {
                             //�������д�Լ��������֤�߼�
                             //return m != null && m.FirstOrDefault().Equals(builder.Configuration["audience"]);  
                             return true;
                         },
                         LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                         {
                             //return notBefore <= DateTime.Now
                             //&& expires >= DateTime.Now;
                             ////&& validationParameters

                             return true;

                         }//�Զ���У�����
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
