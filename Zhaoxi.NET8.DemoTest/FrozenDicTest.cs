using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.NET8.DemoTest
{

    //nuget: BenchmarkDotNet
    [SimpleJob(RunStrategy.ColdStart, iterationCount: 5)]
    public class FrozenDicTest
    {
        public static Dictionary<string, string> dic = new()
        {
           { "name1","oec2003"},
           { "name2","oec2004"},
           { "name3","oec2005"}
        };

        public static FrozenDictionary<string, string> fdic = dic.ToFrozenDictionary();


        [Benchmark]
        public void TestDic()
        {
            for (int i = 0; i < 100000000; i++)
            {
                dic.TryGetValue("name", out _);
            }
        }

        [Benchmark]
        public void TestFDic()
        {
            for (int i = 0; i < 100000000; i++)
            {
                fdic.TryGetValue("name", out _); 
                //fdic["name4"] = "1234";  //不允许只读
                //fdic["name1"] = "1234"; //不允许修改
            }
        }
    }
}
