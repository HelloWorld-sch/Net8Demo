using Zhaoxi.NET8.Business.Interfaces; 
using System;
using System.Collections.Generic;
using System.Linq;
using Zhaoxi.NET8.AutofacExtension;
using Autofac.Extras.DynamicProxy;
using Zhaoxi.NET8.AutofacExtension.AOP;


namespace Zhaoxi.NET8.Business.Services
{

    [Intercept(typeof(CustomInterceptor))]
    public class ApplePhone : IPhone
    {
       
        public IPower Power { get; set; }

        [CusotmSelect] //标记这个特性就表示要做属性注入
        public IMicrophone Microphone { get; set; }
         

        public IHeadphone Headphone { get; set; }
       
        public ApplePhone(IHeadphone iHeadphone)
        {
            Headphone = iHeadphone;
            Console.WriteLine("{0}带参数构造函数", GetType().Name);
        }

        public virtual void Call()
        {
            Console.WriteLine("{0}打电话", GetType().Name); ;
        }

        public void Text()
        {
            Console.WriteLine("{0}发信息", GetType().Name); ;
        }


        public object QueryUser(object opara)
        {
            return new
            {
                Id = 123,
                Name = "Richard",
                DateTiem = DateTime.Now.ToString()
            };
        }

        /// <summary>
        /// 如果创建完毕这个ApplePhone之后，能够自动的去执行这个方法
        /// </summary>
        /// <param name="iPower"></param>
        public void Init123456678890(IPower iPower)
        {
            Power = iPower;
        }
    }
}
