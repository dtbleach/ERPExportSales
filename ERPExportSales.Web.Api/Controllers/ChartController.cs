using ERPExportSales.Entities;
using ERPExportSales.Framework;
using ERPExportSales.Services;
using ERPExportSales.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace ERPExportSales.Web.Api.Controllers
{
    public class ChartController : Controller
    {
        public IChartService chartService;

        public ChartController(IChartService chartService)
        {
            this.chartService = chartService;
        }

        public JsonResult GetQ195()
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
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUSDCNY()
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
            return Json(list);
        }

        public JsonResult GetNewUSDCNY()
        {
            var usdcny = chartService.GetNewUSDCNY();
            return Json(usdcny);
        }

        public JsonResult GetNewQ195()
        {
            var q195 = chartService.GetNewQ195();
            q195.PublishDate = DateTime.Parse(q195.PublishDate).ToString("yyyy-MM-dd");
            return Json(q195);
        }
    }
}
