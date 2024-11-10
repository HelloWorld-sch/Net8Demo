using Zhaoxi.NET8.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.NET8.Business.Services
{
    public class AndroidPhone : IPhone
    {
        public IMicrophone Microphone { get; set; }
        public IHeadphone Headphone { get; set; }
        public IPower Power { get; set; }

        public AndroidPhone()
        {
            Console.WriteLine("{0}构造函数", GetType().Name);
        }

        public void Call()
        {
            Console.WriteLine("{0}打电话", GetType().Name); ;
        }

        public void Init123456678890(IPower iPower)
        {
            Power = iPower;
        }

        public void Text()
        {
            Console.WriteLine("{0}发信息", GetType().Name); ;
        }

        public object QueryUser(object opara)
        {
            throw new NotImplementedException();
        }
    }
}
