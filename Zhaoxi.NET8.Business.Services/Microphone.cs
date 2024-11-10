using Zhaoxi.NET8.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.NET8.Business.Services
{
    public class Microphone : IMicrophone
    {
        public Microphone()
        {
            Console.WriteLine($"{GetType().Name}被构造。。");
        }
    }
}
