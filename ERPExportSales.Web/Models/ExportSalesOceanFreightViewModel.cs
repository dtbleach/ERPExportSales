using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPExportSales.Web.Models
{
    public class ExportSalesOceanFreightViewModel
    {
        public int FID { get; set; }

        public string Port { get; set; }

        public decimal Freight { get; set; }

        public string Schedule { get; set; }

        public int DaysToPort { get; set; }

        public int CustomerID { get; set; }
    }
}