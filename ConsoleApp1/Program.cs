using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
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
                ////url
                //var url = new Uri("http://192.168.1.14:8088/chart/getq195");
                ////设置webapi的用户名和密码
                //// response
                //var response = httpClient.GetAsync(url).Result;
                //var data = response.Content.ReadAsStringAsync().Result;
                ////return data;//接口调用成功数据
                //Console.WriteLine(data);

                Console.WriteLine(获取数据库信息("服务器"));
                Console.WriteLine(获取数据库信息("数据库"));
                Console.WriteLine(获取数据库信息("用户名"));
                Console.WriteLine(获取数据库信息("密码"));
            }
        }

        private static string 密钥 = "Longda86";

        public static string 加密(string 文本)
        {
            if (文本.Length == 0) return string.Empty;

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.Default.GetBytes(文本);

                des.Key = ASCIIEncoding.ASCII.GetBytes(密钥);
                des.IV = ASCIIEncoding.ASCII.GetBytes(密钥);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        StringBuilder ret = new StringBuilder();
                        foreach (byte b in ms.ToArray())
                        {
                            ret.AppendFormat("{0:X2}", b);
                        }
                        return ret.ToString();
                    }
                }
            }
        }

        public static string 解密(string 文本)
        {
            if (文本.Length == 0) return string.Empty;

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = new byte[文本.Length / 2];
                for (int x = 0; x < 文本.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(文本.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                des.Key = ASCIIEncoding.ASCII.GetBytes(密钥);
                des.IV = ASCIIEncoding.ASCII.GetBytes(密钥);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();

                        return System.Text.Encoding.Default.GetString(ms.ToArray());
                    }
                }
            }
        }

        private static string 获取数据库信息(string 类型)
        {
            return 解密(数据库配置.Select("类型 = '" + 加密("正式") + "'")[0][类型].ToString());
        }

        private static DataTable _数据库配置 = null;
        public static DataTable 数据库配置
        {
            get
            {
                if (_数据库配置 == null)
                {
                    _数据库配置 = new DataTable("ds");
                    _数据库配置.Columns.Add("类型");
                    _数据库配置.Columns.Add("服务器");
                    _数据库配置.Columns.Add("数据库");
                    _数据库配置.Columns.Add("用户名");
                    _数据库配置.Columns.Add("密码");

                    _数据库配置.ReadXml(@"D:\Workspace\ERP\ERPExportSales\ConsoleApp1\Config.xml");
                }
                return _数据库配置;
            }
        }
    }
}
