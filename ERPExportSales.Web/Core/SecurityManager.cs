using ERPExportSales.Entities;
using ERPExportSales.Framework;
using ERPExportSales.Repositories;
using ERPExportSales.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ERPExportSales.Web.Core
{
    public static class SecurityManager
    {
        private const string _alg = "HmacSHA256";
        private const string _salt = "rz8LuOtFBXphj9WQfvFh";
        private static int _expirationMinutes = 60;
        private static ApplicationDbContext db = new ApplicationDbContext();


        public static string GenerateToken(string username, string password, string ip, string userAgent, long ticks)
        {
            string hash = string.Join(":", new string[] { username, ip, userAgent, ticks.ToString() });
            string hashLeft = "";
            string hashRight = "";

            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                hmac.Key = Encoding.UTF8.GetBytes(GetHashedPassword(password));
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));

                hashLeft = Convert.ToBase64String(hmac.Hash);
                hashRight = string.Join(":", new string[] { username, ticks.ToString() });
            }

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashLeft, hashRight)));
        }

        public static string GetHashedPassword(string password)
        {
            string key = string.Join(":", new string[] { password, _salt });

            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                // Hash the key.
                hmac.Key = Encoding.UTF8.GetBytes(_salt);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(key));

                return Convert.ToBase64String(hmac.Hash);
            }
        }

        public static bool VerifyToken(string token,HttpRequestBase request)
        {
            string key = Encoding.UTF8.GetString(Convert.FromBase64String(token));

            string[] parts = key.Split(new char[] { ':' });

            if (parts.Length == 3)
            {
                string hash = parts[0];
                string username = parts[1];
                long ticks = 0; long.TryParse(parts[2], out ticks);

                //string password = _employeeService.GetEmployeeSHAPassword(username);
                var user = db.EmployeeEntities.Where(p => p.LoginName == username).FirstOrDefault();
                string password = ConvertHelper.BytesToString(user.Password, Encoding.UTF8);;
                bool cookieExpires = CookieHelper.CookieExpires(username);

                if (!cookieExpires)
                {
                    string computedToken = GenerateToken(username, password, CommonManager.GetIP(request), request.UserAgent, ticks);

                    var loginToken = db.ExportSalesLoginTokenEntities.Where(p => p.UserName == username).FirstOrDefault();
                   // var loginToken = _tokenService.GetExportSalesLoginToken(username);
                    if (null != loginToken)
                    {
                        if (token == computedToken && token == loginToken.Token)
                        {
                           
                            SessionHelper.Add<Employee>("User", user);
                            return true;
                        }
                    }

                }

            }
            return false;
        }
    }
}
