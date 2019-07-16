using ERPExportSales.Entities;
using ERPExportSales.Framework;
using ERPExportSales.Services;
using ERPExportSales.Web.Api.Filter;
using ERPExportSales.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPExportSales.Web.Api.Controllers
{
    [WebApiInfoFilter]
    [RoutePrefix("api/sfc")]
    public class SFCController : ApiController
    {
        public IChartService chartService;
        public IExportSalesService exportSalesService;
        public SFCController(IChartService chartService, IExportSalesService exportSalesService)
        {
            this.chartService = chartService;
            this.exportSalesService = exportSalesService;
        }

        [HttpGet]
        [Route("q195")]
        public MyJsonResult GetQ195()
        {
            IList<ChartViewModel> list = new List<ChartViewModel>();
            var listQ195 = chartService.GetAllQ195();
            if (listQ195 != null && listQ195.Count > 0)
            {
                foreach (var item in listQ195)
                {
                    item.PublishDate = DateTime.Parse(item.PublishDate).ToString("yyyy-MM-dd");
                    var q195 = ConvertHelper.Trans<Q195, ChartViewModel>(item);
                    list.Add(q195);
                }
            }
            return new MyJsonResult(list); //Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("usdcny")]
        public MyJsonResult GetUSDCNY()
        {
            IList<ChartViewModel> list = new List<ChartViewModel>();
            var listUSDCNY = chartService.GetAllUSDCNY();
            if (listUSDCNY != null && listUSDCNY.Count > 0)
            {
                foreach (var item in listUSDCNY)
                {
                    var usdcny = ConvertHelper.Trans<USDCNY, ChartViewModel>(item);
                    list.Add(usdcny);
                }
            }
            return new MyJsonResult(list); //Json(list);
        }

        [HttpGet]
        [Route("nusdcny")]
        public MyJsonResult GetNewUSDCNY()
        {
            var usdcny = chartService.GetNewUSDCNY();
            return new MyJsonResult(usdcny);//return Json(usdcny);
        }

        [HttpGet]
        [Route("nq195")]
        public MyJsonResult GetNewQ195()
        {
            var q195 = chartService.GetNewQ195();
            q195.PublishDate = DateTime.Parse(q195.PublishDate).ToString("yyyy-MM-dd");
            return new MyJsonResult(q195);// json(q195);
        }

        //[HttpGet]
        //[Route("fg")]
        //public MyJsonResult Freight()
        //{
        //    int userType = 0;
        //    int.TryParse(user.UserType, out userType);
        //    IList<VExportSalesOceanFreight> freights = new List<VExportSalesOceanFreight>();
        //    if (userType == 1)
        //    {
        //        freights = exportSalesService.GetExportSalesOceanFreightByEmployee(user.UserName);
        //    }
        //    else if (userType == 2)
        //    {
        //        freights = exportSalesService.GetExportSalesOceanFreightByCustomerID(user.ID);
        //    }

        //    IList<ExportSalesOceanFreightViewModel> list = new List<ExportSalesOceanFreightViewModel>();
        //    if (freights != null && freights.Count > 0)
        //    {
        //        foreach (var item in freights)
        //        {
        //            var freight = ConvertHelper.Trans<VExportSalesOceanFreight, ExportSalesOceanFreightViewModel>(item);
        //            list.Add(freight);
        //        }
        //    }
        //    return PartialView(freights);
        //}

        [HttpGet]
        [Route("ph")]
        public MyJsonResult PublicHoliday()
        {
            var holiday = exportSalesService.GetPublicHoliday();

            return new MyJsonResult(holiday);
        }

    }
}
