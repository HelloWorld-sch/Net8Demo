using Autofac;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.NET8.AutofacExtension;
using Zhaoxi.NET8.AutofacExtension.AOP;
using Zhaoxi.NET8.Business.Interfaces;
using Zhaoxi.NET8.Business.Services;

namespace Zhaoxi.NET8.DemoTest
{
    /// <summary>
    /// 
    /// </summary>
    public class AutofacTest
    {
        //Autofac--是第三方一个组件 

        //1. nuget: 
        //      autofac
        //      Autofac.Extensions.DependencyInjection
        public static void Show()
        {
            //基本使用
            {
                /////建造者
                //ContainerBuilder containerBuilder = new ContainerBuilder();
                ////注册抽象和具体之间的关系
                //containerBuilder.RegisterType<Microphone>().As<IMicrophone>();
                ////得到容器的最终实例
                //IContainer container = containerBuilder.Build();
                //IMicrophone microphone = container.Resolve<IMicrophone>();
            }

            //多种注册
            {
                //{
                //    ContainerBuilder containerBuilder = new ContainerBuilder();
                //    containerBuilder.RegisterType<Microphone>().As<IMicrophone>();
                //    IContainer container = containerBuilder.Build();
                //    IMicrophone microphone = container.Resolve<IMicrophone>();
                //}

                ////注册一个具体的实例
                //{
                //    ContainerBuilder containerBuilder = new ContainerBuilder();
                //    containerBuilder.RegisterInstance<IMicrophone>(new Microphone());
                //    IContainer container = containerBuilder.Build();
                //    IMicrophone microphone = container.Resolve<IMicrophone>();
                //}

                ////注册一段逻辑
                //{
                //    ContainerBuilder containerBuilder = new ContainerBuilder();
                //    containerBuilder.RegisterType<Microphone>().As<IMicrophone>();
                //    containerBuilder.Register<IPower>(context =>
                //    {
                //        IMicrophone microphone1 = context.Resolve<IMicrophone>();
                //        return new Power(microphone1);
                //    });

                //    IContainer container = containerBuilder.Build();
                //    IPower power = container.Resolve<IPower>();
                //}

                ////注册泛型
                //{
                //    ContainerBuilder containerBuilder = new ContainerBuilder();
                //    containerBuilder.RegisterGeneric(typeof(List<>)).As(typeof(IList<>));
                //    containerBuilder.RegisterType<Microphone>().As<IMicrophone>();
                //    IContainer container = containerBuilder.Build();
                //    IList<IMicrophone> microphonelist = container.Resolve<IList<IMicrophone>>();
                //}

                ////注册程序集
                //{
                //    ContainerBuilder containerBuilder = new ContainerBuilder();
                //    Assembly interfaceAssembly = Assembly.LoadFrom("Zhaoxi.NET8.Business.Interfaces.dll");
                //    Assembly serviceAssembly = Assembly.LoadFrom("Zhaoxi.NET8.Business.Services.dll");

                //    containerBuilder.RegisterAssemblyTypes(interfaceAssembly, serviceAssembly)
                //        .AsImplementedInterfaces();

                //    IContainer container = containerBuilder.Build();
                //    IMicrophone microphone = container.Resolve<IMicrophone>();
                //    IHeadphone headphone = container.Resolve<IHeadphone>();
                //    IPower power = container.Resolve<IPower>();
                //}
            }

            //构造函数注入---构造函数参数有依赖，自动创建出来，然后传递进去
            //属性注入
            //方法注入
            // Autofac 全部支持的
            {
                //构造函数注入
                {
                    //ContainerBuilder containerBuilder = new ContainerBuilder();
                    //containerBuilder.RegisterType<Microphone>().As<IMicrophone>();
                    //containerBuilder.RegisterType<Power>().As<IPower>();
                    //IContainer container = containerBuilder.Build();
                    //IPower power = container.Resolve<IPower>();
                }

                //属性注入--创建好的对象，如果有特定的属性，就自动把属性对应的类型也创建实例，赋值给属性
                {
                    //ContainerBuilder containerBuilder = new ContainerBuilder();
                    //containerBuilder.RegisterType<Microphone>().As<IMicrophone>();
                    //containerBuilder.RegisterType<Power>().As<IPower>();
                    //containerBuilder.RegisterType<Headphone>().As<IHeadphone>();

                    //containerBuilder.RegisterType<ApplePhone>().As<IPhone>() 
                    //     .PropertiesAutowired();  //支持属性注入

                    //IContainer container = containerBuilder.Build();
                    //IPhone power = container.Resolve<IPhone>();
                }
                //属性注入--支持PropertySelector
                {
                    //ContainerBuilder containerBuilder = new ContainerBuilder();
                    //containerBuilder.RegisterType<Microphone>().As<IMicrophone>();
                    //containerBuilder.RegisterType<Power>().As<IPower>();
                    //containerBuilder.RegisterType<Headphone>().As<IHeadphone>();

                    //containerBuilder.RegisterType<ApplePhone>().As<IPhone>()
                    //     .PropertiesAutowired(new CustomPropertySelector());  //支持属性注入

                    //IContainer container = containerBuilder.Build();
                    //IPhone power = container.Resolve<IPhone>();
                }

                //方法注入--在执行方法的时候，自动把参数创建好传递进去，在创建完毕一个类之后，自动的吧某个特定的方法去执行下。 
                {
                    //ContainerBuilder containerBuilder = new ContainerBuilder();
                    //containerBuilder.RegisterType<Microphone>().As<IMicrophone>();
                    //containerBuilder.RegisterType<Power>().As<IPower>();
                    //containerBuilder.RegisterType<Headphone>().As<IHeadphone>();

                    //containerBuilder.RegisterType<ApplePhone>().As<IPhone>()
                    //     .PropertiesAutowired(new CustomPropertySelector())   //支持属性注入
                    //     .OnActivated(activa =>
                    //     {
                    //         IPower power1= activa.Context.Resolve<IPower>();
                    //         activa.Instance.Init123456678890(power1);
                    //     });

                    //IContainer container = containerBuilder.Build();
                    //IPhone power = container.Resolve<IPhone>(); 
                    ////依赖注入 、注入数据的时候，构造函数注入，最早赋值；   属性注入其次；  方法注入在最后；
                }
            }

            //Autofac--单抽象多实现
            {
                //ContainerBuilder containerBuilder = new ContainerBuilder();

                ////containerBuilder.RegisterType<MicrophoneNew>().As<IMicrophone>();
                ////containerBuilder.RegisterType<Microphone>().As<IMicrophone>();

                ////IContainer containe = containerBuilder.Build(); 
                ////IMicrophone microphone = containe.Resolve<IMicrophone>();
                ////IEnumerable<IMicrophone> microphonelist = containe.Resolve<IEnumerable<IMicrophone>>();

                //containerBuilder.RegisterType<MicrophoneNew>().Keyed<IMicrophone>("MicrophoneNew");
                //containerBuilder.RegisterType<Microphone>().Keyed<IMicrophone>("Microphone");

                //IContainer containe = containerBuilder.Build();
                //IMicrophone microphone1= containe.ResolveKeyed<IMicrophone>("MicrophoneNew");
                //IMicrophone microphone2 = containe.ResolveKeyed<IMicrophone>("Microphone");

            }

            //AOP:  面向切面编程---动态的增加新功能，不用修改原有的代码；
            //Autofac： 两种方式，通过类扩展，通过接口扩展
            {

                ContainerBuilder containerBuilder = new ContainerBuilder(); 
                containerBuilder.RegisterType<Microphone>().As<IMicrophone>();
                containerBuilder.RegisterType<Power>().As<IPower>();
                containerBuilder.RegisterType<Headphone>().As<IHeadphone>();
                containerBuilder.RegisterType<ApplePhone>().As<IPhone>()
                    //.EnableInterfaceInterceptors();  //通过接口的方式来完成AOP扩展  特点：只要是实现了这个接口的类，内部所有的方法调用都会走AOP流程；

                    .EnableClassInterceptors(); //通过类的方式来完成AOP扩展, 必须在实现类上制定扩展的interceptor; 实现类中，方法如果要走AOP流程，必须定义为虚方法；

                containerBuilder.RegisterType<CustomInterceptor>(); 
                IContainer containe = containerBuilder.Build();
                IPhone phone = containe.Resolve<IPhone>();
                phone.QueryUser(new { id=123,Name="Richard" });
                phone.Call();
                phone.Text();
            }
        }
    }
}
