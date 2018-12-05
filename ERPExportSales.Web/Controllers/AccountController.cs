using ERPExportSales.Core;
using ERPExportSales.Entities;
using ERPExportSales.Framework;
using ERPExportSales.Services;
using ERPExportSales.Web.Core;
using ERPExportSales.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPExportSales.Web.Controllers
{
    public class AccountController : Controller
    {
        public IExportSalesUserService userService;
        public IEmployeeService employeeService;
        public IExportSalesLoginTokenService tokenService;
        public ICustomerService customerService;
        public IIPWhiteListService ipWhiteListService;
        public AccountController(IExportSalesUserService userService, IEmployeeService employeeService,IExportSalesLoginTokenService tokenService, ICustomerService customerService, IIPWhiteListService ipWhiteListService)
        {
            this.userService = userService;
            this.employeeService = employeeService;
            this.tokenService = tokenService;
            this.customerService = customerService;
            this.ipWhiteListService = ipWhiteListService;
        }
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            int userType = 0;
            string code = SessionHelper.Get("CheckCode");
            if (model.ValidatedCode == null)
            {
                ViewBag.ErrorMessage = "ValidatedCode error";
                return View(model);
            }
            if (!model.ValidatedCode.Equals(code))
            {
                ViewBag.ErrorMessage = "ValidatedCode error";
                return View(model);
            }
           var result = userService.Login(model.LoginName, model.Password,ref userType);
           string ip = CommonManager.GetIP(Request);
            LoggerHelper.Info("{'IP':'" + ip + "','Msg':'获取IP'}");
            if (result.Result)
            {
                if (userType == 1)
                {
                    bool ipFlag = ipWhiteListService.IsIPExistWhiteList(ip);
                    if (!ipFlag)
                    {
                        SessionHelper.Del("User");
                        SessionHelper.Del("CheckCode");
                        CookieHelper.ClearCookie("token");
                        CookieHelper.ClearCookie("rememberLogin");
                        ViewBag.ErrorMessage = "Employees are not allowed to visit this website on the Internet";
                        LoggerHelper.Info("{'IP':'" + ip + "','Name':'" + model.LoginName + "','UserType':" + userType + ",'Date:'" + DateTime.Now + "','Msg':" + result.Message + "}");
                        return View(model);
                    }
                }

                if (model.RememberMe)
                {
                    long ticks = new DateTime().Ticks;
                   
                    string userAgent = CommonManager.GetUserAgent(Request);
                    string shaPassword = string.Empty;
                    if(userType==1)
                            shaPassword = employeeService.GetEmployeeSHAPassword(model.LoginName);
                    else if(userType==2)
                             shaPassword = customerService.GetCustomerSHAPassword(model.LoginName);
                    string token = SecurityManager.GenerateToken(model.LoginName, shaPassword, ip, userAgent, userType.ToString(),ticks);
                    DateTime expries = DateTime.Now.AddDays(1.0);
                    CookieHelper.SetCookie("token", token, expries);
                    CookieHelper.SetCookie("rememberLogin", "true", expries);
                    ExportSalesLoginToken loginToken = new ExportSalesLoginToken();
                    loginToken.ExpiresTime = expries;
                    loginToken.Token = token;
                    loginToken.UserName = model.LoginName;
                    loginToken.UserType = userType;
                    tokenService.SaveLoginToken(loginToken);
                    LoggerHelper.Info("{'IP':'" + ip + "','Name':'" + model.LoginName + "','UserType':"+userType+",'Date:'"+DateTime.Now+ "','Msg':" + result.Message + "}");
                }else
                {
                    UserViewModel user = new UserViewModel();
                    if (userType == 1)
                    {
                        var employee = employeeService.GetEmployee(model.LoginName);
                        user.ID = employee.FID;
                        user.LoginName = employee.LoginName;
                        user.Password = employee.Password;
                        user.UserName = employee.Name;
                        user.UserType = userType.ToString();
                        user.DepID = employee.DepID;
                    }else if (userType == 2)
                    {
                        var customer = customerService.GetCustomer(model.LoginName);
                        user.ID = customer.fID;
                        user.LoginName = customer.LoginName;
                        user.Password = customer.Password;
                        user.UserName = customer.Name;
                        user.UserType = userType.ToString();
                    }
                    SessionHelper.Add("NickName", user.UserName);
                    SessionHelper.Add("User", user);
                    SessionHelper.Del("CheckCode");
                    CookieHelper.SetCookie("rememberLogin", "false");
                    CookieHelper.ClearCookie("token");
                    LoggerHelper.Info("{'IP':'" + ip + "','Name':'" + model.LoginName + "','UserType':" + userType + ",'Date:'" + DateTime.Now + "','Msg':"+ result.Message+ "}");
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                SessionHelper.Del("User");
                SessionHelper.Del("NickName");
                SessionHelper.Del("CheckCode");
                CookieHelper.ClearCookie("token");
                CookieHelper.ClearCookie("rememberLogin");
                ViewBag.ErrorMessage = result.Message;
                LoggerHelper.Info("{'IP':'" + ip + "','Name':'" + model.LoginName + "','UserType':" + userType + ",'Date:'" + DateTime.Now + "','Msg':" + result.Message + "}");
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            SessionHelper.Del("User");
            SessionHelper.Del("CheckCode");
            SessionHelper.Del("NickName");
            CookieHelper.ClearCookie("token");
            CookieHelper.ClearCookie("rememberLogin");
            return RedirectToAction("Login");
        }
    }
}