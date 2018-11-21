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
using Webdiyer.WebControls.Mvc;

namespace ERPExportSales.Web.Controllers
{
    public class HomeController : Controller
    {
        public IExportSalesUserService userService;
        public IExportSalesService exportSalesService;
        public HomeController(IExportSalesUserService userService, IExportSalesService exportSalesService)
        {
            this.exportSalesService = exportSalesService;
            this.userService = userService;
        }

        [AuthorizeUser]
        public ActionResult Index(int pageNum=1)
        {
            
            ViewBag.PageNum = pageNum;
            if (pageNum < 1)
            {
                ViewBag.PageNum = 1;
            }
            var user = SessionHelper.Get<Employee>("User");
            UserViewModel userModel = new UserViewModel();
            userModel.LoginName = user.LoginName;
            var freights = exportSalesService.GetExportSalesOceanFreight(2496);
            IList<ExportSalesOceanFreightViewModel> list = new List<ExportSalesOceanFreightViewModel>();
            if (freights != null && freights.Count > 0)
            {
                foreach (var item in freights)
                {
                    var freight=ConvertHelper.Trans<VExportSalesOceanFreight, ExportSalesOceanFreightViewModel>(item);
                    list.Add(freight);
                }
            }

            IList<PublicHolidayViewModel> publicHolidayList = new List<PublicHolidayViewModel>();
            var holiday = exportSalesService.GetPublicHoliday();
            if (holiday != null && holiday.Count > 0)
            {
                foreach (var item in holiday)
                {
                    var publicHoliday = ConvertHelper.Trans<VPublicHoliday, PublicHolidayViewModel>(item);
                    publicHolidayList.Add(publicHoliday);
                }
            }
            ExportSalesModel model = new ExportSalesModel();
            model.UserModel = userModel;
            model.FreightList = list;
            model.PublicHoliday = publicHolidayList;
            //int count = 0;
             var orders = exportSalesService.GetOrdersByEmployeeName("周晓东", 50,pageNum);
            IList<OrderViewModel> orderList = new List<OrderViewModel>();
            foreach (var item in orders)
            {
                var order = ConvertHelper.Trans<Order, OrderViewModel>(item);
                orderList.Add(order);
            }
            //var orderViewModels = new PagedList<OrderViewModel>(orderList, pageNum, 10, count);
            model.OrderList = orderList;
          
            return View(model);
        }


    }
}