
using Newtonsoft.Json;
using Zhaoxi.NET8.WebCore;
using Zhaoxi.NET8.WebMinimalApi.Utility;

namespace Zhaoxi.NET8.WebMinimalApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<ICustomJWTService, CustomHSJWTService>();//�ǶԳ�
            builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapPost("Login", (string name, string password, ICustomJWTService _iJWTService) =>
            {
                if ("Richard".Equals(name) && "123456".Equals(password))
                {
                    //�����ݿ��в�ѯ������
                    CurrentUser user = new CurrentUser()
                    {
                        Id = 123,
                        Name = "Richard",
                        Age = 36,
                        NikeName = "���ƽ�ʦRichard��ʦ",
                        Description = ".NET�ܹ�ʦ",
                        RoleList = "admin"
                    };

                    string token = _iJWTService.GetToken(user);
                    return JsonConvert.SerializeObject(new
                    {
                        result = true,
                        token
                    });
                }
                else
                {
                    return JsonConvert.SerializeObject(new
                    {
                        result = false,
                        token = ""
                    });
                }
            });

            app.Run();
        }
    }
}
