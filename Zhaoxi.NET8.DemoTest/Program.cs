using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text.Json;
using Zhaoxi.NET8.Business.Interfaces;
using Zhaoxi.NET8.Business.Services;

namespace Zhaoxi.NET8.DemoTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //内置IOC容器
                {
                    //ServiceCollectTest.Show();
                }

                {
                    //AutofacTest.Show();
                }


                {
                    //SerializerTest.Show();
                }

                {
                    //NET8New.Show();
                }

                {
                    BenchmarkRunner.Run<FrozenDicTest>(); 
                }
                Console.ReadKey();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
