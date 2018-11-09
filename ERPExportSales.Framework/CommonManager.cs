using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ERPExportSales.Framework
{
    public static class CommonManager
    {
        public static string GetIP(HttpRequestBase request)
        {
            string ip = request.Headers["X-Forwarded-For"];

            if (string.IsNullOrEmpty(ip))
            {
                ip = request.UserHostAddress;
            }

            return ip;
        }

        public static string GetUserAgent(HttpRequestBase request)
        {
            string userAgent = request.Headers["User-Agent"];

            if (string.IsNullOrEmpty(userAgent))
            {
                userAgent = request.UserAgent;
            }

            return userAgent;
        }
    }
}
