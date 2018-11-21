using ERPExportSales.Entities;
using Webdiyer.WebControls.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPExportSales.Web.Models
{
    public class ExportSalesModel
    {
        public UserViewModel UserModel { get; set; }

        public IList<ExportSalesOceanFreightViewModel> FreightList { get; set; }

        public IList<PublicHolidayViewModel> PublicHoliday { get; set; }

        public IList<OrderViewModel> OrderList { get; set; }
    }

    public class QueryViewModel
    {
        public string PONo { get; set; }

        public string SCNo { get; set; }

        public string InvoiceNo{ get; set; }
    }
}