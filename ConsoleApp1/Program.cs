using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var httpClient = new HttpClient())
            {
                //url
                var url = new Uri("http://192.168.1.14:8088/chart/getq195");
                //设置webapi的用户名和密码
                // response
                var response = httpClient.GetAsync(url).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                //return data;//接口调用成功数据
                Console.WriteLine(data);
            }
        }
    }
}
