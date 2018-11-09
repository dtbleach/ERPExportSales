using ERPExportSales.Entities;
using ERPExportSales.Framework;
using ERPExportSales.Services;
using ERPExportSales.Web.Filter;
using ERPExportSales.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPExportSales.Web.Controllers
{
    public class HomeController : Controller
    {
        public IExportSalesUserService userService;
        public HomeController(IExportSalesUserService userService)
        {
            this.userService = userService;
        }

        [AuthorizeUser]
        public ActionResult Index()
        {
            var user = SessionHelper.Get<Employee>("User");
            UserViewModel model = new UserViewModel();
            model.LoginName = user.LoginName;
            return View(model);
        }
    }
}