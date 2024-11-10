using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Zhaoxi.NET8.DemoTest
{
    internal class SerializerTest
    {
        public static void Show()
        {
            JsonSerializerOptions options0 = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            JsonSerializerOptions options1 = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower,
            };
            JsonSerializerOptions options2 = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.KebabCaseUpper,
            };
            JsonSerializerOptions options3 = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            };
            JsonSerializerOptions options4 = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseUpper,
            };

            //输出：{"userName":"oec2003"}           //{"userName":"oec2003"}    序列化后，首字母变成小写
            Console.WriteLine(JsonSerializer.Serialize(new UserInfo() { UserName = "oec2003" }, options0));

            //输出：{"user-name":"oec2003"}          把大写字母开头的拆分为两个单词，用-连接，首字母变成小写
            Console.WriteLine(JsonSerializer.Serialize(new UserInfo() { UserName = "oec2003" }, options1));

            //输出：{"USER-NAME":"oec2003"}           把大写字母开头的拆分为两个单词，用-连接，且所有的字母均变成大写
            Console.WriteLine(JsonSerializer.Serialize(new UserInfo() { UserName = "oec2003" }, options2));

            //输出：{"user_name":"oec2003"}           把大写字母开头的拆分为两个单词，用_连接，且所有的字母均变成小写
            Console.WriteLine(JsonSerializer.Serialize(new UserInfo() { UserName = "oec2003" }, options3));

            //输出：{"USER_NAME":"oec2003"}             把大写字母开头的拆分为两个单词，用_连接，且所有的字母均变成大写
            Console.WriteLine(JsonSerializer.Serialize(new UserInfo() { UserName = "oec2003" }, options4));
        }
    }
}
