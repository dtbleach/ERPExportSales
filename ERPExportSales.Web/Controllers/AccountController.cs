using ERPExportSales.Services;
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
        public AccountController(IExportSalesUserService userService)
        {
            this.userService = userService;
        }
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var result=userService.Login(model.LoginName, model.Password);
            if (result.Result)
            {
               return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }
    }
}