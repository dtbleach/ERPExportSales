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


        public static string GenerateToken(string username, string password, string ip, string userAgent, string userType,long ticks)
        {
            string hash = string.Join(":", new string[] { username, ip, userAgent, userType, ticks.ToString() });
            string hashLeft = "";
            string hashRight = "";

            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                hmac.Key = Encoding.UTF8.GetBytes(GetHashedPassword(password));
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));

                hashLeft = Convert.ToBase64String(hmac.Hash);
                hashRight = string.Join(":", new string[] { username, userType, ticks.ToString() });
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

            if (parts.Length == 4)
            {
                string hash = parts[0];
                string username = parts[1];
                string userType = parts[2];
                long ticks = 0; long.TryParse(parts[3], out ticks);
                LoggerHelper.Info("{'msg':'开始进入attr验证2',usertype:'" + userType + "','username':'"+username+"'}");
                //string password = _employeeService.GetEmployeeSHAPassword(username);
                Models.UserViewModel model = new Models.UserViewModel();
                if (int.Parse(userType) == 1)
                {
                    string ip = request.UserHostAddress;
                    if (!ip.Equals("::1"))
                    {
                        int i = ip.LastIndexOf(".");
                        ip = ip.Substring(0,i);
                        var iplist = db.IPWhiteListEntities.Select(p => p.IP).ToList();
                        if (!iplist.Contains(ip))
                        {
                            LoggerHelper.Info("{'msg':'开始进入attr验证3',ip:'" + ip + "','msg':'ip错误'}");
                            return false;
                        }
                    }
                    LoggerHelper.Info("{'msg':'开始进入attr验证3',ip:'" + ip + "'}");
                    Employee user = db.EmployeeEntities.Where(p => p.LoginName == username).FirstOrDefault();
                    model.LoginName = user.LoginName;
                    model.UserName = user.Name;
                    model.Password = user.Password;
                    model.UserType = userType;
                    model.ID = user.FID;
                    model.DepID = user.DepID;

                }else if (int.Parse(userType) == 2)
                {
                    Customer customer = db.CustomerEntities.Where(p => p.LoginName == username).FirstOrDefault();
                    model.LoginName = customer.LoginName;
                    model.UserName = customer.Name;
                    model.Password = customer.Password;
                    model.UserType = userType;
                    model.ID = customer.fID;
                }

                string password = ConvertHelper.BytesToString(model.Password, Encoding.UTF8);;
                bool cookieExpires = CookieHelper.CookieExpires("token");
                LoggerHelper.Info("{'msg':'开始进入attr验证4',cookieExpires:'" + cookieExpires + "'}");
                if (!cookieExpires)
                {
                    string computedToken = GenerateToken(username, password, CommonManager.GetIP(request), request.UserAgent, userType,ticks);
                    var loginToken = db.ExportSalesLoginTokenEntities.Where(p => p.UserName == username).FirstOrDefault();
                   // var loginToken = _tokenService.GetExportSalesLoginToken(username);
                    if (null != loginToken)
                    {
                        LoggerHelper.Info("{'用户':'" + loginToken.UserName + "'.'数据库token':'" + loginToken.Token + "','CookieToken':'" + token + "'}");
                        if (token == computedToken && token == loginToken.Token)
                        {
                            SessionHelper.Add<Models.UserViewModel>("User", model);
                            return true;
                        }
                    }else
                    {
                        LoggerHelper.Info("{'用户':'"+ loginToken .UserName+ "','msg':loginToken为null}");
                    }

                }

            }
            return false;
        }
    }
}
