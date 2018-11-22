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
        public ActionResult Index()
        {
            var user = SessionHelper.Get<Employee>("User");
            UserViewModel userModel = new UserViewModel();
            userModel.LoginName = user.LoginName;

            ExportSalesModel model = new ExportSalesModel();
            model.UserModel = userModel;
            model.QueryModel = new QueryViewModel();
            return View(model);
        }

        [AuthorizeUser]
        public PartialViewResult OrderQuery(ExportSalesModel model, string InvoiceNo,bool IsBtnQuery=true, int pageNum = 1)
        {
            if (model == null) {
                model = new ExportSalesModel();
                model.QueryModel = new QueryViewModel();
                model.UserModel = new UserViewModel();
            }
            if (model.QueryModel == null)
            {
                model.QueryModel = new QueryViewModel();
            }
            ViewBag.PageNum = pageNum;
            if (pageNum < 1)
            {
                ViewBag.PageNum = 1;
            }
            if (IsBtnQuery)
            {
                SessionHelper.Add("QueryWhere", model.QueryModel);
            }else
            {
                model.QueryModel = SessionHelper.Get<QueryViewModel>("QueryWhere");
            }

            var orders = exportSalesService.GetOrdersByEmployeeName("周晓东", 50, pageNum, model.QueryModel.PONo, model.QueryModel.SCNo, model.QueryModel.InvoiceNo);
            IList<OrderViewModel> orderList = new List<OrderViewModel>();
            foreach (var item in orders)
            {
                var order = ConvertHelper.Trans<Order, OrderViewModel>(item);
                orderList.Add(order);
            }

            return PartialView(orderList);
        }


        [AuthorizeUser]
        public PartialViewResult Freight()
        {
            var freights = exportSalesService.GetExportSalesOceanFreight(2496);
            IList<ExportSalesOceanFreightViewModel> list = new List<ExportSalesOceanFreightViewModel>();
            if (freights != null && freights.Count > 0)
            {
                foreach (var item in freights)
                {
                    var freight = ConvertHelper.Trans<VExportSalesOceanFreight, ExportSalesOceanFreightViewModel>(item);
                    list.Add(freight);
                }
            }
            return PartialView(list);
        }

        [AuthorizeUser]
        public PartialViewResult PublicHoliday()
        {
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

            return PartialView(publicHolidayList);
        }
    }
}