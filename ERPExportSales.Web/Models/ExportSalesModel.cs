using ERPExportSales.Entities;
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
}