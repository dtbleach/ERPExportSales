using ERPExportSales.Configuration;
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
        public IExportSalesConfiguration config;
        public HomeController(IExportSalesUserService userService, IExportSalesService exportSalesService, IExportSalesConfiguration config)
        {
            this.exportSalesService = exportSalesService;
            this.userService = userService;
            this.config = config;
        }

        [AuthorizeUser]
        public ActionResult Index()
        {
            var user = SessionHelper.Get<UserViewModel>("User");
            if(user!=null)
                ViewBag.NickName = user.UserName;
            ExportSalesModel model = new ExportSalesModel();
            model.UserModel = user;
            model.QueryModel = new QueryViewModel();
            model.OrderList = new List<OrderViewModel>();
            return View(model);
        }

        [AuthorizeUser]
        public PartialViewResult OrderQuery(ExportSalesModel model,bool IsBtnQuery=true, int pageNum = 1)
        {
            var user = SessionHelper.Get<UserViewModel>("User");

            if (user == null)
            {
                return PartialView();
            }

            if (model == null)
            {
                model = new ExportSalesModel();
                model.QueryModel = new QueryViewModel();
                model.UserModel = user;
            }
            else
            {
                if (model.QueryModel == null)
                {
                    model.QueryModel = new QueryViewModel();
                }else
                {
                    ViewBag.ScNo = model.QueryModel.SCNo;
                    ViewBag.PoNo = model.QueryModel.PONo;
                    ViewBag.InvoiceNo = model.QueryModel.InvoiceNo;
                }


                if (model.UserModel == null)
                {
                    model.UserModel = user;
                }
            }
            ViewBag.PageNum = pageNum;
            if (pageNum < 1)
            {
                ViewBag.PageNum = 1;
                pageNum = 1;
            }
            if (IsBtnQuery)
            {
                if (!string.IsNullOrEmpty(model.QueryModel.Customer))
                    model.QueryModel.Customer = model.QueryModel.Customer;
                if (!string.IsNullOrEmpty(model.QueryModel.InvoiceNo))
                    model.QueryModel.InvoiceNo = model.QueryModel.InvoiceNo.Trim();
                if (!string.IsNullOrEmpty(model.QueryModel.PONo))
                    model.QueryModel.PONo = model.QueryModel.PONo.Trim();
                if (!string.IsNullOrEmpty(model.QueryModel.SCNo))
                    model.QueryModel.SCNo = model.QueryModel.SCNo.Trim();
                SessionHelper.Add("QueryWhere", model.QueryModel);
            }else
            {
                model.QueryModel = SessionHelper.Get<QueryViewModel>("QueryWhere");
            }

            int userType = 0;
            int.TryParse(model.UserModel.UserType, out userType);
            IList<Order> orders = new List<Order>();
            if (userType == 1)
            {
                orders = exportSalesService.GetOrdersByEmployeeName(model.UserModel.UserName,model.UserModel.DepID, 50, pageNum, model.QueryModel.PONo, model.QueryModel.SCNo, model.QueryModel.InvoiceNo,model.QueryModel.Customer);
            }else if (userType == 2)
            {
                orders = exportSalesService.GetOrdersByCustomerID(model.UserModel.ID, 50, pageNum, model.QueryModel.PONo, model.QueryModel.SCNo, model.QueryModel.InvoiceNo);
            }
            IList<OrderViewModel> orderList = new List<OrderViewModel>();
            foreach (var item in orders)
            {
                var order = ConvertHelper.Trans<Order, OrderViewModel>(item);
                order.SCNoHref = Encryption64.Encrypt(item.SCNo + ":" + config.OrderFolderName, config.Encryption64Key);
                order.InvoiceHref = Encryption64.Encrypt(item.InvoiceNo + ":" + config.InvoiceFolderName, config.Encryption64Key);
                order.PackingHref = Encryption64.Encrypt(item.InvoiceNo + ":" + config.PackingFolderName, config.Encryption64Key);
                order.BLNoHref = Encryption64.Encrypt(item.InvoiceNo + ":" + config.BLFolderName, config.Encryption64Key);
                order.QRHref = Encryption64.Encrypt(item.InvoiceNo + ":" + config.QRFolderName, config.Encryption64Key);
                orderList.Add(order);
            }
            model.OrderList = orderList;
            return PartialView(model);
        }


        [AuthorizeUser]
        public PartialViewResult Freight()
        {
            var user = SessionHelper.Get<UserViewModel>("User");

            if (user == null)
            {
                return PartialView();
            }
            int userType = 0;
            int.TryParse(user.UserType,out userType);
            IList<VExportSalesOceanFreight> freights = new List<VExportSalesOceanFreight>();
            if (userType == 1)
            {
                freights= exportSalesService.GetExportSalesOceanFreightByEmployee(user.UserName);
            }
            else if (userType == 2)
            {
                freights = exportSalesService.GetExportSalesOceanFreightByCustomerID(user.ID);
            }
           
            IList<ExportSalesOceanFreightViewModel> list = new List<ExportSalesOceanFreightViewModel>();
            if (freights != null && freights.Count > 0)
            {
                foreach (var item in freights)
                {
                    var freight = ConvertHelper.Trans<VExportSalesOceanFreight, ExportSalesOceanFreightViewModel>(item);
                    list.Add(freight);
                }
            }
            return PartialView(freights);
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