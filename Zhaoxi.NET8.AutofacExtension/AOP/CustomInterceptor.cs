using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.NET8.AutofacExtension.AOP
{
    public class CustomInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            {
                Console.WriteLine("=================================");
                Console.WriteLine("===========在XX业务逻辑前执行====");
                Console.WriteLine("=================================");
            }
            invocation.Proceed(); //这句话的执行就是去执行目标方法
            {
                Console.WriteLine("=================================");
                Console.WriteLine("===========在XX业务逻辑后执行====");
                Console.WriteLine("=================================");
            }
        }
    }
}
