using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Zhaoxi.NET8.DemoTest
{
    internal class NET7Test
    {
        public static void Show()
        {
          
            {
                IDerived derived = new DerivedImplement();
                derived.Base = 1;
                derived.Derived = 2;

                Console.WriteLine(JsonSerializer.Serialize<IDerived>(derived)); //输出结果： {"Derived":2}
            } 
        }
    }

    public interface IBase
    {
        public int Base { get; set; }
    }


    public interface IDerived : IBase
    {
        public int Derived { get; set; }
    }

    public class DerivedImplement : IDerived
    {
        public int Base { get; set; }
        public int Derived { get; set; }
    }
}
