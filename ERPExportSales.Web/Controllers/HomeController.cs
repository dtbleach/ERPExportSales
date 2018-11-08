using ERPExportSales.Services;
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
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}