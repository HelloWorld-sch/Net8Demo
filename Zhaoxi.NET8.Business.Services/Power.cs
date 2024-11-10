using Zhaoxi.NET8.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.NET8.Business.Services
{
    public class Power : IPower
    {
        private IMicrophone Microphone;
        private IMicrophone Microphone2;

        public Power(IMicrophone microphone)
        {
            Console.WriteLine($"{GetType().Name}被构造。。");
            Microphone = microphone;
        }
         
        //public Power(IMicrophone microphone, IMicrophone microphone2)
        //{
        //    Console.WriteLine($"{GetType().Name}被构造。。");
        //    Microphone = microphone;
        //    Microphone2 = microphone2;
        //}
    }
}
