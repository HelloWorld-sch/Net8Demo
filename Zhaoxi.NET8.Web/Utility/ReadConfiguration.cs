using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhaoxi.NET8.Web.Utility
{
    /// <summary>
    /// 1.通过Configuration读取配置文件
    /// </summary>
    public class ReadConfiguration
    {
        public static void Show(IConfiguration Configuration)
        {
            //var list = Configuration.GetSection("ConnectionStrings:ReadConnectionList");

            Console.WriteLine($"Id:{Configuration["Id"]}");
            Console.WriteLine($"Name:{Configuration["Name"]}");
            Console.WriteLine($"TeachInfo.Id:{Configuration["TeachInfo:Id"]}");
            Console.WriteLine($"TeachInfo.Name:{Configuration["TeachInfo:Name"]}");
            Console.WriteLine($"ConnectionStrings.WriteConnection:{Configuration["ConnectionStrings:WriteConnection"]}");
            Console.WriteLine($"ConnectionStrings.ReadConnectionList1:{Configuration["ConnectionStrings:ReadConnectionList:0"]}");
            Console.WriteLine($"ConnectionStrings.ReadConnectionList2:{Configuration["ConnectionStrings:ReadConnectionList:1"]}");
            Console.WriteLine($"ConnectionStrings.ReadConnectionList3:{Configuration["ConnectionStrings:ReadConnectionList:2"]}");
        }

        public static void ShowBind(IConfiguration Configuration)
        {
            ConnectionStringOptions options = new ConnectionStringOptions();
            Configuration.Bind("ConnectionStrings", options);

            Console.WriteLine($"ConnectionStrings.WriteConnection:{options.WriteConnection}");
            Console.WriteLine($"ConnectionStrings.ReadConnectionList1:{options.ReadConnectionList[0]}");
            Console.WriteLine($"ConnectionStrings.ReadConnectionList2:{options.ReadConnectionList[1]}");
            Console.WriteLine($"ConnectionStrings.ReadConnectionList3:{options.ReadConnectionList[2]}");
        }


    }
}
