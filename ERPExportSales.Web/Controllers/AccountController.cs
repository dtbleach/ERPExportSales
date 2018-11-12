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
        public AccountController(IExportSalesUserService userService, IEmployeeService employeeService,IExportSalesLoginTokenService tokenService)
        {
            this.userService = userService;
            this.employeeService = employeeService;
            this.tokenService = tokenService;
        }
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var result = userService.Login(model.LoginName, model.Password);
            if (result.Result)
            {
                if (model.RememberMe)
                {
                    long ticks = new DateTime().Ticks;
                    string ip = CommonManager.GetIP(Request);
                    string userAgent = CommonManager.GetUserAgent(Request);

                    string shaPassword = employeeService.GetEmployeeSHAPassword(model.LoginName);

                    string token = SecurityManager.GenerateToken(model.LoginName, shaPassword, ip, userAgent, ticks);
                    DateTime expries = DateTime.Now.AddDays(1.0);
                    CookieHelper.SetCookie("token", token, expries);
                    CookieHelper.SetCookie("rememberLogin", "true");
                    ExportSalesLoginToken loginToken = new ExportSalesLoginToken();
                    loginToken.ExpiresTime = expries;
                    loginToken.Token = token;
                    loginToken.UserName = model.LoginName;
                    tokenService.SaveLoginToken(loginToken);
                }else
                {
                    var employee = employeeService.GetEmployee(model.LoginName);
                    SessionHelper.Add("User", employee);
                    CookieHelper.SetCookie("rememberLogin", "false");
                    CookieHelper.ClearCookie("token");
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }
    }
}