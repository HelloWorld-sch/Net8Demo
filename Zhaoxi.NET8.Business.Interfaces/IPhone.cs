
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.NET8.AutofacExtension.AOP;

namespace Zhaoxi.NET8.Business.Interfaces
{

    //[Intercept(typeof(CustomInterceptor))]
    public interface IPhone
    {
        void Call();

        void Text();

        void Init123456678890(IPower iPower);

        IMicrophone Microphone { get; set; }
        IHeadphone Headphone { get; set; }
        IPower Power { get; set; }
        object QueryUser(object opara);
    }
}
