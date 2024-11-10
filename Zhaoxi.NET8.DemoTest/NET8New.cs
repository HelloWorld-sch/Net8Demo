using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Zhaoxi.NET8.DemoTest
{
    internal class NET8New
    {
        public static void Show()
        {
            {
                //ReadOnlySpan<string> colors = new[] { "Red", "Green", "Blue", "Black" };
                //string[] t1 = Random.Shared.GetItems(colors, 10);
                //Console.WriteLine(JsonSerializer.Serialize(t1));
                ////输出： 每次都会不一样
                //Console.ReadKey();
            }

            {
                //string[] colors = new[] { "Red", "Green", "Blue", "Black" };
                //Random.Shared.Shuffle(colors);
                //Console.WriteLine(JsonSerializer.Serialize(colors));
            }

            {
                IDerived derived = new DerivedImplement();
                derived.Base = 1;
                derived.Derived = 2;

                Console.WriteLine(JsonSerializer.Serialize<IDerived>(derived)); //输出结果： {"Derived":2,"Base":1}
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
