using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.NET8.Business.Interfaces;
using Zhaoxi.NET8.Business.Services;

namespace Zhaoxi.NET8.DemoTest
{
    public class ServiceCollectTest
    {
        public static void Show()
        {
            //内置IOC容器
            //让程序调用方，使用抽象，可以获取到具体的实例
            //其实就是用来创建对象的，------工厂；
            //使用IOC容器的第一步

            {
                //1.创建IOC容器实例
                IServiceCollection services = new ServiceCollection();
                //2.注册抽象和具体之间的关系
                services.AddTransient<IMicrophone, Microphone>();

                //3.Build下得到IOC容器的具体实例
                //最后要通过IOC容器来创建对象的实例 
                ServiceProvider serviceProvider = services.BuildServiceProvider();
                //最终创建的对象实例---是一个具体的对象

                //4.通过抽象获取具体实例
                IMicrophone microphone = serviceProvider.GetService<IMicrophone>();
            }

            //依赖注入
            //对象对象A依赖于对象B， 对象B依赖于对象C, 对象C依赖于对象D.....
            //如果要通过IOC容器来创建对象A，自动创建对象C，传递给对象B，创建出对象B，传递给对象A，最终可以创建出对象A;

            //通过构造函数来依赖----构造函数的注入
            {
                IServiceCollection services = new ServiceCollection();
                //2.注册抽象和具体之间的关系
                services.AddTransient<IMicrophone, Microphone>();
                services.AddTransient<IPower, Power>();
                services.AddTransient<IHeadphone, Headphone>();
                services.AddTransient<IPhone, ApplePhone>();

                ServiceProvider serviceProvider = services.BuildServiceProvider();
                //IPower power = serviceProvider.GetService<IPower>(); 
                IPhone phone = serviceProvider.GetService<IPhone>();
            }

            //构造函数注入
            //属性注入---.NET Core没支持，.NET8也没有
            //方法注入---.NET7 


            //IOC容器的生命周期
            {

                //1.瞬时生命周期---每一次创建的实例都是一个全新的实例
                //  可以采用 AddTransient   TryAddTransient 来注册即可 
                {
                    //IServiceCollection services = new ServiceCollection();
                    //services.TryAddTransient<IMicrophone, Microphone>();
                    //ServiceProvider serviceProvider = services.BuildServiceProvider();
                    //IMicrophone microphone1 = serviceProvider.GetService<IMicrophone>();
                    //IMicrophone microphone2 = serviceProvider.GetService<IMicrophone>();
                    //IMicrophone microphone3 = serviceProvider.GetService<IMicrophone>();
                    //IMicrophone microphone4 = serviceProvider.GetService<IMicrophone>();
                    //Console.WriteLine(object.ReferenceEquals(microphone1, microphone2));
                    //Console.WriteLine(object.ReferenceEquals(microphone2, microphone3));
                    //Console.WriteLine(object.ReferenceEquals(microphone1, microphone4));
                }


                //2.单例生命周期，每次创建的实例都是同一个实例，
                //  可以通过AddSingleton   或者  TryAddSingleton 来注册即可
                {
                    //IServiceCollection services = new ServiceCollection();
                    //services.TryAddSingleton<IMicrophone, Microphone>();
                    //ServiceProvider serviceProvider = services.BuildServiceProvider();
                    //IMicrophone microphone1 = serviceProvider.GetService<IMicrophone>();
                    //IMicrophone microphone2 = serviceProvider.GetService<IMicrophone>();
                    //IMicrophone microphone3 = serviceProvider.GetService<IMicrophone>();
                    //IMicrophone microphone4 = serviceProvider.GetService<IMicrophone>();
                    //Console.WriteLine("******************************************************");
                    //Console.WriteLine(object.ReferenceEquals(microphone1, microphone2));
                    //Console.WriteLine(object.ReferenceEquals(microphone2, microphone3));
                    //Console.WriteLine(object.ReferenceEquals(microphone1, microphone4));
                }

                //3.作用域生命周期.  同一个ServiceProvider 获取到的实例是同一个实例
                //                   不同的ServiceProvider 获取到的实例是不是同一个实例
                //  可以通过AddScoped   或者  TryAddScoped 来注册即可
                {
                    //IServiceCollection services = new ServiceCollection();
                    //services.TryAddScoped<IMicrophone, Microphone>();
                    //ServiceProvider serviceProvider = services.BuildServiceProvider();
                    //IMicrophone microphone1 = serviceProvider.GetService<IMicrophone>();
                    //IMicrophone microphone2 = serviceProvider.GetService<IMicrophone>();

                    //ServiceProvider serviceProvider1 = services.BuildServiceProvider();

                    //IMicrophone microphone3 = serviceProvider1.GetService<IMicrophone>();
                    //IMicrophone microphone4 = serviceProvider1.GetService<IMicrophone>();
                    //Console.WriteLine("******************************************************");
                    //Console.WriteLine(object.ReferenceEquals(microphone1, microphone2));
                    //Console.WriteLine(object.ReferenceEquals(microphone3, microphone4));
                    //Console.WriteLine(object.ReferenceEquals(microphone1, microphone4));

                }


                //单抽象多实现

                //在.NET8 有升级---支持了别名，在注册的时候，可以给注册的抽象具体赋值一个别名；
                //获取的时候也必须要带上别名才能获取；
                {
                    IServiceCollection services = new ServiceCollection();
                    //services.AddTransient<IMicrophone, MicrophoneNew>();
                    //services.AddTransient<IMicrophone, Microphone>();

                    services.AddKeyedTransient<IMicrophone, MicrophoneNew>("MicrophoneNew");
                    services.AddKeyedTransient<IMicrophone, Microphone>("Microphone");

                    ServiceProvider serviceProvider = services.BuildServiceProvider();

                    IMicrophone microphone1 = serviceProvider.GetKeyedService<IMicrophone>("MicrophoneNew");
                    IMicrophone microphone2 = serviceProvider.GetKeyedService<IMicrophone>("Microphone");


                    //IMicrophone microphone = serviceProvider.GetService<IMicrophone>(); 
                    //IEnumerable<IMicrophone> microphonelist = serviceProvider.GetService<IEnumerable<IMicrophone>>();

                    //如果注册了多个，但是我想要获取单个前面的  实例

                }

                //多种注册方式
                //1.多生命周期的支持
                //2.别名的支持

                {
                    //IServiceCollection services = new ServiceCollection();
                    //services.AddTransient(typeof(IMicrophone), typeof(Microphone));

                    ////可以注册一段逻辑
                    //services.AddTransient(typeof(IPower), provider =>
                    //{
                    //    IMicrophone microphone = provider.GetService<IMicrophone>();
                    //    return new Power(microphone);
                    //});

                    //ServiceProvider serviceProvider = services.BuildServiceProvider();
                    //object oIntance = serviceProvider.GetService(typeof(IMicrophone));
                    //object oPower = serviceProvider.GetService(typeof(IPower));
                }

                {
                    IServiceCollection services = new ServiceCollection();
                    services.AddTransient(typeof(Microphone));
                    ServiceProvider serviceProvider = services.BuildServiceProvider();
                    object oIntance = serviceProvider.GetService(typeof(Microphone));
                }

            }
        }
    }
}
