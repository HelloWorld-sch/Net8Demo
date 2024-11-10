using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Zhaoxi.NET7.Consoles.Service;
using Zhaoxi.NET8.DemoTest;

namespace Zhaoxi.NET7.Consoles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                //IServiceCollection services = new ServiceCollection();
                //services.AddTransient<IMicrophoneNew, MicrophoneNew>();
                //services.AddTransient<IMicrophoneNew, MicrophoneNew1>();
                //ServiceProvider serviceProvider = services.BuildServiceProvider();

                //IMicrophoneNew microphone = serviceProvider.GetService<IMicrophoneNew>();
                //IEnumerable<IMicrophoneNew> microphonelist = serviceProvider.GetService<IEnumerable<IMicrophoneNew>>();
            }

            //.NET7时代的 序列化器的支持
            {

                //JsonSerializerOptions options1 = new JsonSerializerOptions
                //{
                //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                //};
                //Console.WriteLine(JsonSerializer.Serialize(new UserInfo() { UserName = "oec2003" }, options1));
            }

            NET7Test.Show();
        }
    }
}
