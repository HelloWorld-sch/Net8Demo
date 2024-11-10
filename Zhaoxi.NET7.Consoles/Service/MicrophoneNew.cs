using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.NET7.Consoles.Service
{
    public class MicrophoneNew : IMicrophoneNew
    {
        public MicrophoneNew()
        {
            Console.WriteLine($"{GetType().Name}被构造。。");
        }
    }
}
